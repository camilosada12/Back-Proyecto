using Business.Interfaces.BusinessRegister;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Interfaces.IJWT;
using Business.Mensajeria;
using Business.Mensajeria.Interfaces;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.Auth;
using Entity.DTOs.Default.GoogleTokenDto;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Default.RegisterRequestDto;
using Entity.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Management;
using System.Text;
using Utilities.Custom;
using Utilities.Exceptions;
namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class LoginController : ControllerBase
    {


        private readonly IToken _token;
        private readonly IUserService _userService;
        private readonly ILogger<LoginController> _logger;
        private readonly EncriptePassword _utilities;
        private readonly JwtSettings _jwt; // para ValidarToken
        //private readonly IServiceEmail _serviceEmail;
        //private readonly INotifyManager _notifyManager;

        public LoginController(EncriptePassword utilities,IToken token, ILogger<LoginController> logger, IUserService userService, ApplicationDbContext context, EncriptePassword utilidades, IOptions<JwtSettings> jwtOptions) //, IServiceEmail serviceEmail, INotifyManager notifyManager)
        {
            _token = token;
            _userService = userService;
            _logger = logger;
            _utilities = utilities;
            _jwt = jwtOptions.Value;

            //_serviceEmail = serviceEmail;
            //_notifyManager = notifyManager;
        }

        [HttpPost]
        [Route("Registrarse")]
        [ProducesResponseType(typeof(RegisterResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Registrarse([FromBody] RegisterRequestDto request)
        {
            try
            {
                var result = await _userRegistrationService.RegisterAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null) message += " | Inner: " + ex.InnerException.Message;
                return BadRequest(new { isSuccess = false, message });
            }
        }



        [HttpPost("Email")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> LoginEmail([FromBody] EmailLoginDto login)
        {
            try
            {
                var (access, refresh, csrf) = await _token.GenerateTokensAsync(login);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    isSuccess = true,
                    accessToken = access,
                    refreshToken = refresh,
                    csrfToken = csrf
                });
                //return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { isSuccess = false, message = "Credenciales inválidas." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en LoginDocumento");
                return StatusCode(500, new { isSuccess = false, message = "Error interno." });
            }
        }



        [HttpGet]
        [Route("ValidarToken")]
        public IActionResult ValidarToken([FromQuery] string token)

        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key)),
                    ValidateIssuer = true,
                    ValidIssuer = _jwt.issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwt.audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                handler.ValidateToken(token, parameters, out _);
                return Ok(new { isSuccess = true });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Token inválido");
                return Ok(new { isSuccess = false, message = "Token inválido" });
            }

        }


        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleTokenDto tokenDto)
        {
            var payload = await _token.VerifyGoogleToken(tokenDto.TokenId);
            if (payload == null)
                return Unauthorized("Token inválido de Google");

            // tu flujo crea/recupera usuario con Google
            var user = await _userService.createUserGoogle(payload.Email, payload.Name);

            // generar tokens del sistema (reutilizando GenerateTokensAsync)
            var login = new LoginDto { email = user.email, password = user.password };
            var (access, refresh, csrf) = await _token.GenerateTokensAsync(login);

            return Ok(new { accessToken = access, refreshToken = refresh, csrfToken = csrf });
        }

    }
}
