using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Interfaces.IJWT;
using Data.Interfaces.IDataImplement.Security;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.LoginDto;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Utilities.Exceptions; // ValidationException, ExternalServiceException
using System.Linq;

namespace Business.Custom
{
    public class TokenBusiness : IToken
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _dataUser;
        private readonly IRolUserRepository _userRepository;

        public TokenBusiness(
            IConfiguration configuration,
            IRolUserRepository userRepository,
            IUserRepository dataUser)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _dataUser = dataUser;
        }

        // ====== LOGIN POR EMAIL+PASSWORD ======
        public async Task<string> GenerateTokenEmail(EmailLoginDto dto)
        {
            if (dto is null) throw new ValidationException("Solicitud inválida.");
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new ValidationException("Email y contraseña son obligatorios.");

            var email = dto.Email.Trim().ToLowerInvariant();
            var pass = dto.Password.Trim();

            // Busca por email
            var user = await _dataUser.FindByEmailAsync(email);
            if (user is null) throw new UnauthorizedAccessException("Credenciales inválidas.");

            // Mientras estés con seeds en claro, podrías comparar directo.
            // Recomendado: usar verificador real (BCrypt/PBKDF2/etc.)
            var ok = await _dataUser.VerifyPasswordAsync(user, pass);
            if (!ok) throw new UnauthorizedAccessException("Credenciales inválidas.");

            var roles = await _userRepository.GetJoinRolesAsync(user.id);
            return await BuildJwtAsync(user, roles);
        }

        // ====== LOGIN POR DOCUMENTO ======
        public async Task<string> GenerateTokenDocumento(DocumentLoginDto dto)
        {
            if (dto is null) throw new ValidationException("Solicitud inválida.");
            if (dto.DocumentTypeId <= 0 || string.IsNullOrWhiteSpace(dto.DocumentNumber))
                throw new ValidationException("Tipo y número de documento son obligatorios.");

            var docNum = dto.DocumentNumber.Trim();

            // Busca por documento (NO valida password)
            var user = await _dataUser.FindByDocumentAsync(dto.DocumentTypeId, docNum);
            if (user is null) throw new UnauthorizedAccessException("Credenciales inválidas.");

            var roles = await _userRepository.GetJoinRolesAsync(user.id);
            return await BuildJwtAsync(user, roles);
        }

        // ====== Construcción del JWT (reutilizable) ======
        private Task<string> BuildJwtAsync(User user, IEnumerable<string> roles)
        {
            var key = _configuration["Jwt:key"];
            if (string.IsNullOrWhiteSpace(key))
                throw new ExternalServiceException("Configuración JWT incompleta: falta Jwt:key.");

            if (!double.TryParse(_configuration["Jwt:exp"], out var expMinutes) || expMinutes <= 0)
                throw new ExternalServiceException("Configuración JWT inválida: Jwt:exp debe ser minutos > 0.");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("uid", user.id.ToString())
            };

            if (!string.IsNullOrWhiteSpace(user.email))
                claims.Add(new Claim(ClaimTypes.Email, user.email));

            if (user.documentTypeId.HasValue)
                claims.Add(new Claim("doc_type_id", user.documentTypeId.Value.ToString()));
            if (!string.IsNullOrWhiteSpace(user.documentNumber))
                claims.Add(new Claim("doc_number", user.documentNumber));

            foreach (var r in roles.Where(r => !string.IsNullOrWhiteSpace(r)).Distinct(StringComparer.OrdinalIgnoreCase))
                claims.Add(new Claim(ClaimTypes.Role, r));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expMinutes),
                signingCredentials: credentials
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwt));
        }

        public async Task<IEnumerable<string>> GetUserRoles(int idUser)
        {
            var roles = await _userRepository.GetJoinRolesAsync(idUser);
            return roles;
        }

        public bool validarToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["Jwt:key"];
            if (string.IsNullOrWhiteSpace(key)) return false;

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Verifica la validez de un token JWT generado por Google (ID Token).
        /// </summary>
        /// <param name="tokenId">ID Token recibido desde Google en el frontend</param>
        /// <returns>Payload del token si es válido; null si no lo es</returns>
        //public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string tokenId)
        //    {
        //        try
        //        {
        //            var settings = new GoogleJsonWebSignature.ValidationSettings
        //            {
        //                // Lista de IDs de cliente válidos registrados en Google Cloud Console
        //                Audience = new List<string> { "436268030419-5q7o4a4lv3ahg2p12iad63ubptlka6pu.apps.googleusercontent.com" }
        //            };

        //            // Valida el token contra la audiencia esperada
        //            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId, settings);
        //            return payload;
        //        }
        //        catch
        //        {
        //            // Si el token es inválido o no se pudo validar, retorna null
        //            return null;
        //        }
        //    }

        
        }
    }
