using Business.Interfaces.BusinessRegister;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Interfaces.IJWT;
using Business.Mensajeria;
using Business.Mensajeria.Interfaces;
using Business.Services.Auth;
using Entity.DTOs.Default.Auth.LoginResultDto;
using Entity.DTOs.Default.Auth.RegisterReponseDto;
using Entity.DTOs.Default.GoogleTokenDto;
using Entity.DTOs.Default.LoginDto;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Default.RegisterRequestDto;
using Entity.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRegistrationService _userRegistrationService;
        //private readonly IServiceEmail _serviceEmail;
        //private readonly INotifyManager _notifyManager;

        public LoginController(EncriptePassword utilities,IToken token, ILogger<LoginController> logger, IUserService userService, ApplicationDbContext context, EncriptePassword utilidades,
            IUserRegistrationService userRegistrationService) //, IServiceEmail serviceEmail, INotifyManager notifyManager)
        {
            _token = token;
            _userService = userService;
            _logger = logger;
            _utilities = utilities;
            _userRegistrationService = userRegistrationService;
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
                var token = await _token.GenerateTokenEmail(login);
                return Ok(new { isSuccess = true, token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { isSuccess = false, message = "Credenciales inválidas." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en LoginEmail");
                return StatusCode(500, new { isSuccess = false, message = "Error interno." });
            }
        }

        [HttpPost("Documento")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> LoginDocumento([FromBody] DocumentLoginDto login)
        {
            try
            {
                var token = await _token.GenerateTokenDocumento(login);
                return Ok(new { isSuccess = true, token });
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

            bool respuesta = _token.validarToken(token);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });

        }



        //[HttpPost("google")]
        //public async Task<IActionResult> GoogleLogin([FromBody] GoogleTokenDto tokenDto)
        //{
        //    // 1. Validar el token recibido de Google
        //    var payload = await _token.VerifyGoogleToken(tokenDto.TokenId);

        //    // 2. Si el token no es válido, rechazar el acceso
        //    if (payload == null)
        //        return Unauthorized("Token inválido de Google");

        //    // 3. Buscar o crear un usuario en la base de datos con el email recibido en el token de Google
        //    var user = await _userService.createUserGoogle(payload.Email, payload.Name);

        //    // 4. Obtener los roles asociados a ese usuario
        //    //var roles = await _token.GetRolesByUserId(user.Id);
        //    //var permissions = await _rolUserResvice.GetPermissionsByUserId(user.Id);

        //    var login = new LoginDto
        //    {
        //        email = user.email,
        //        password = user.password,
        //    };
        //    // 5. Generar un JWT del sistema con los datos del usuario y sus roles
        //    var token = await _token.GenerateToken(login);

        //    // 6. Retornar el token generado para que el frontend lo use en sesiones autenticadas
        //    return Ok(new { token });
        //}

    }
}
