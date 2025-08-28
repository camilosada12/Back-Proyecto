    using Business.Interfaces.IJWT;
    using Data.Interfaces.IDataImplement.Security;
    using Entity.DTOs.Default.LoginDto;
    using Google.Apis.Auth;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
using Utilities.Exceptions;

    namespace Business.Custom
    {
        public class TokenBusiness : IToken
        {
            private readonly IConfiguration _configuration;
            private readonly IUserRepository _dataUser;
            private readonly IRolUserRepository _userRepository;

            public TokenBusiness(IConfiguration configuration, IRolUserRepository userRepository, IUserRepository dataUser)
            {
                _configuration = configuration;
                _userRepository = userRepository;
                _dataUser = dataUser;
            }
        public async Task<string> GenerateToken(LoginDto dto)
        {
            // —— 1) Determinar modo de autenticación (Google va aparte) ——
            bool usesEmail = !string.IsNullOrWhiteSpace(dto.Email) || !string.IsNullOrWhiteSpace(dto.Password);
            bool usesDoc = (dto.DocumentTypeId.HasValue) || !string.IsNullOrWhiteSpace(dto.DocumentNumber);

            int modes = (usesEmail ? 1 : 0) + (usesDoc ? 1 : 0);
            if (modes != 1)
                throw new ValidationException("Proporcione exactamente un modo de autenticación: (Email+Password) o (DocumentTypeId+DocumentNumber).");


            // —— 2) Validación de campos mínimos por modo ——
            if (usesEmail)
            {
                if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                    throw new ValidationException("Email y contraseña son obligatorios.");
            }
            if (usesDoc)
            {
                if (!dto.DocumentTypeId.HasValue || string.IsNullOrWhiteSpace(dto.DocumentNumber))
                    throw new ValidationException("Tipo y número de documento son obligatorios.");
            }

            // —— 3) Validar credenciales vía repositorio existente ——
            // Asumimos que _dataUser.ValidateUserAsync(dto) ya soporta ambos modos.
            var user = await _dataUser.ValidateUserAsync(dto);
            if (user is null)
                throw new ValidationException("Credenciales inválidas.");

            // —— 4) Roles ——
            var roles = await GetUserRoles(user.id);

            // —— 5) Claims seguros (evitar NRE y normalizar) ——
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
        new Claim("uid", user.id.ToString())
    };

            if (!string.IsNullOrWhiteSpace(user.email))
                claims.Add(new Claim(ClaimTypes.Email, user.email));

            // Si moviste documento a User, agrega estos (opcionales)
            if (user.documentTypeId.HasValue)
                claims.Add(new Claim("doc_type_id", user.documentTypeId.Value.ToString()));
            if (!string.IsNullOrWhiteSpace(user.documentNumber))
                claims.Add(new Claim("doc_number", user.documentNumber));

            foreach (var r in roles.Where(r => !string.IsNullOrWhiteSpace(r)).Distinct(StringComparer.OrdinalIgnoreCase))
                claims.Add(new Claim(ClaimTypes.Role, r));

            // —— 6) Config JWT robusta ——
            var key = _configuration["Jwt:key"];
            if (string.IsNullOrWhiteSpace(key))
                throw new ExternalServiceException("Configuración JWT incompleta: falta Jwt:key.");

            var expStr = _configuration["Jwt:exp"];
            if (!double.TryParse(expStr, out var expMinutes) || expMinutes <= 0)
                throw new ExternalServiceException("Configuración JWT inválida: Jwt:exp debe ser minutos > 0.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


        public async Task<IEnumerable<string>> GetUserRoles(int idUser)
            {
                var roles = await _userRepository.GetJoinRolesAsync(idUser);
                return roles; // devuelve la lista directamente
            }


            public bool validarToken(string token)
            {
                var ClaimsPrincipal = new ClaimsPrincipal();
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!))
                };

                try
                {
                    ClaimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                    return true;
                }
                catch (SecurityTokenExpiredException)
                {
                    //Manejar token Expirado
                    return false;
                }
                catch (SecurityTokenInvalidSignatureException)
                {
                    // Manejar firma Invalida
                    return false;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }


            /// <summary>
            /// Verifica la validez de un token JWT generado por Google (ID Token).
            /// </summary>
            /// <param name="tokenId">ID Token recibido desde Google en el frontend</param>
            /// <returns>Payload del token si es válido; null si no lo es</returns>
            public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string tokenId)
            {
                try
                {
                    var settings = new GoogleJsonWebSignature.ValidationSettings
                    {
                        // Lista de IDs de cliente válidos registrados en Google Cloud Console
                        Audience = new List<string> { "436268030419-5q7o4a4lv3ahg2p12iad63ubptlka6pu.apps.googleusercontent.com" }
                    };

                    // Valida el token contra la audiencia esperada
                    var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId, settings);
                    return payload;
                }
                catch
                {
                    // Si el token es inválido o no se pudo validar, retorna null
                    return null;
                }
            }

        
        }
    }
