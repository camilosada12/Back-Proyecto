using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Business.Interfaces.IJWT;
using Data.Interfaces.IDataImplement.Security;
using Data.Interfaces.Security;
using Data.Services.Security;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.Auth;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Utilities.Exceptions; // ValidationException, ExternalServiceException
using System.Linq;

namespace Business.Custom
{
    public class TokenBusiness : IToken
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolUserRepository _rolUserRepository;
        private readonly IRefreshTokenRepository _refreshRepo;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<User> _passwordHasher;

        public TokenBusiness(
            IRolUserRepository rolUserRepository,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshRepo,
            IOptions<JwtSettings> jwtSettings,
            IPasswordHasher<User> passwordHasher)
        {
            _rolUserRepository = rolUserRepository;
            _userRepository = userRepository;
            _refreshRepo = refreshRepo;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;

            EnsureSigningKeyStrength(_jwtSettings.key);
        }

        public async Task<(string AccessToken, string RefreshToken, string CsrfToken)> GenerateTokensAsync(LoginDto dto)
        {
            // 1) Validar credenciales
            var user = await _userRepository.FindEmail(dto.email)
                ?? throw new UnauthorizedAccessException("Usuario o contraseña inválida.");

            // Verificación con hasher (user.password es el hash almacenado)
            var pwdResult = _passwordHasher.VerifyHashedPassword(user, user.password, dto.password);
            if (pwdResult == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Usuario o contraseña inválida.");

            // 2) Roles
            var roles = (await _rolUserRepository.GetJoinRolesAsync(user.id)).ToList();

            // 3) Access token
            var accessToken = BuildAccessToken(user, roles);

            // 4) Refresh token (rotación, guardamos HASH HMAC-SHA512)
            var now = DateTime.UtcNow;
            var refreshPlain = GenerateSecureRandomUrlToken(64);
            var refreshHash = HashRefreshToken(refreshPlain);

            var refreshEntity = new RefreshToken
            {
                UserId = user.id,
                TokenHash = refreshHash,
                CreatedAt = now,
                ExpiresAt = now.AddDays(_jwtSettings.refreshTokenExpirationDays)
            };
            await _refreshRepo.AddAsync(refreshEntity);

            // 4.1) Poda: máximo N tokens activos por usuario
            const int maxActiveRefreshTokens = 5;
            var validTokens = (await _refreshRepo.GetValidTokensByUserAsync(user.id)).ToList();
            if (validTokens.Count > maxActiveRefreshTokens)
            {
                foreach (var t in validTokens.Skip(maxActiveRefreshTokens))
                    await _refreshRepo.RevokeAsync(t);
            }

            // 5) CSRF token (double-submit cookie/header)
            var csrf = GenerateSecureRandomUrlToken(32);
            return (accessToken, refreshPlain, csrf);
        }

        public async Task<(string NewAccessToken, string NewRefreshToken)> RefreshAsync(string refreshTokenPlain, string? remoteIp = null)
        {
            var hash = HashRefreshToken(refreshTokenPlain);
            var record = await _refreshRepo.GetByHashAsync(hash)
                ?? throw new SecurityTokenException("Refresh token inválido");

            if (record.ExpiresAt <= DateTime.UtcNow)
                throw new SecurityTokenException("Refresh token expirado");

            if (record.IsRevoked)
            {
                var all = await _refreshRepo.GetValidTokensByUserAsync(record.UserId);
                foreach (var t in all)
                    await _refreshRepo.RevokeAsync(t);

                throw new SecurityTokenException("Refresh token inválido o reutilizado");
            }

            var user = await _userRepository.GetByIdAsync(record.UserId)
                ?? throw new SecurityTokenException("Usuario no encontrado");

            var roles = (await _rolUserRepository.GetJoinRolesAsync(user.id)).ToList();

            var newAccessToken = BuildAccessToken(user, roles);

            var now = DateTime.UtcNow;
            var newRefreshPlain = GenerateSecureRandomUrlToken(64);
            var newRefreshHash = HashRefreshToken(newRefreshPlain);

            var newRefreshEntity = new RefreshToken
            {
                UserId = user.id,
                TokenHash = newRefreshHash,
                CreatedAt = now,
                ExpiresAt = now.AddDays(_jwtSettings.refreshTokenExpirationDays)
            };

            await _refreshRepo.AddAsync(newRefreshEntity);
            await _refreshRepo.RevokeAsync(record, replacedByTokenHash: newRefreshHash);

            return (newAccessToken, newRefreshPlain);
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var hash = HashRefreshToken(refreshToken);
            var record = await _refreshRepo.GetByHashAsync(hash);
            if (record != null && !record.IsRevoked)
                await _refreshRepo.RevokeAsync(record);
        }

        private string BuildAccessToken(User user, IEnumerable<string> roles)
        {
            var now = DateTime.UtcNow;
            var accessExp = now.AddMinutes(_jwtSettings.accessTokenExpirationMinutes);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,   user.id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.email),
            new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,   new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

            foreach (var r in roles.Where(r => !string.IsNullOrWhiteSpace(r)).Distinct())
                claims.Add(new Claim(ClaimTypes.Role, r));

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.issuer,
                audience: _jwtSettings.audience,
                claims: claims,
                notBefore: now,
                expires: accessExp,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private string HashRefreshToken(string token)
        {
            var pepper = Encoding.UTF8.GetBytes(_jwtSettings.key);
            using var hmac = new HMACSHA512(pepper);
            var mac = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToHexString(mac).ToLowerInvariant();
        }

        private static string GenerateSecureRandomUrlToken(int bytesLength)
        {
            var bytes = new byte[bytesLength];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        private static void EnsureSigningKeyStrength(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || Encoding.UTF8.GetByteCount(key) < 32)
                throw new InvalidOperationException("JwtSettings.key debe tener al menos 32 caracteres aleatorios (≥256 bits).");
        }

        public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string tokenId)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { "436268030419-5q7o4a4lv3ahg2p12iad63ubptlka6pu.apps.googleusercontent.com" }
                };
                return await GoogleJsonWebSignature.ValidateAsync(tokenId, settings);
            }
            catch { return null; }
        }
    }

}

