using Business.Mensajeria.Email.@interface;
using Entity.DTOs.Default.Email;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Implements
{
    [ApiController]
    [Route("api/verificacion")]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationService _verificationService;

        public VerificationController(IVerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        /// <summary>
        /// Registro inicial: manda un código de verificación (24h)
        /// </summary>
        [HttpPost("send")]
        public async Task<IActionResult> SendCode([FromBody] SendVerificationDto dto)
        {
            await _verificationService.SendVerificationAsync(dto.Nombre, dto.Email);
            return Ok(new { message = "Código enviado al correo" });
        }

        /// <summary>
        /// Valida el código de verificación (registro inicial o mensual)
        /// </summary>
        [HttpPost("validate")]
        public IActionResult ValidateCode([FromBody] VerificationRequestDto dto)
        {
            var result = _verificationService.ValidateCode(dto.Email, dto.Code);
            return result
                ? Ok(new { valid = true, message = "Cuenta verificada correctamente" })
                : BadRequest(new { valid = false, message = "Código inválido o expirado" });
        }

        /// <summary>
        /// Reactivar cuenta bloqueada con código
        /// </summary>
        [HttpPost("reactivate")]
        public async Task<IActionResult> Reactivate([FromBody] VerificationRequestDto dto)
        {
            var result = await _verificationService.ReactivateAccountAsync(dto.Email, dto.Code);
            return result
                ? Ok(new { reactivated = true, message = "Cuenta reactivada correctamente" })
                : BadRequest(new { reactivated = false, message = "Código inválido o expirado" });
        }

        /// <summary>
        /// Solicitar un nuevo código para reactivar cuenta bloqueada
        /// </summary>
        [HttpPost("send-reactivation")]
        public async Task<IActionResult> SendReactivation([FromBody] SendVerificationDto dto)
        {
            await _verificationService.SendReactivationCodeAsync(dto.Email);
            return Ok(new { message = "Código de reactivación enviado" });
        }

        /// <summary>
        /// Re-verificación mensual manual (ej: disparada por un admin o frontend)
        /// </summary>
        [HttpPost("send-monthly")]
        public async Task<IActionResult> SendMonthly([FromBody] SendVerificationDto dto)
        {
            await _verificationService.SendMonthlyReverificationAsync(dto.Email, dto.Nombre);
            return Ok(new { message = "Código de re-verificación mensual enviado" });
        }
    }
}
