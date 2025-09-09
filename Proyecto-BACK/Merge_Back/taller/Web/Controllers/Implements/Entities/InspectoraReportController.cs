using Business.Interfaces.IBusinessImplements.Entities;
using Business.Interfaces.PDF;
using Business.Services.PDF;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Controllers.ControllersBase.Web.Controllers.BaseController;



using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Implements.Entities
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    [Produces("application/json")]
    public class InspectoraReportController : ControllerBase
    {
        private readonly IInspectoraReportService _service;
        private readonly IPdfGeneratorService _pdfService;
        private readonly ILogger<InspectoraReportController> _logger;

        public InspectoraReportController(
            IInspectoraReportService service,
            IPdfGeneratorService pdf,
            ILogger<InspectoraReportController> logger)
        {
            _service = service;
            _pdfService = pdf;
            _logger = logger;
        }

        // GET: api/InspectoraReport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspectoraReportSelectDto>>> GetAll([FromQuery] GetAllType getAllType)
        {
            var result = await _service.GetAllAsync(getAllType);
            return Ok(result);
        }

        // GET: api/InspectoraReport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InspectoraReportSelectDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/InspectoraReport
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] InspectoraReportDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }

        // PUT: api/InspectoraReport/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] InspectoraReportDto dto)
        {
            var success = await _service.UpdateAsync(dto);
            if (!success) return NotFound();
            return Ok();
        }

        // DELETE: api/InspectoraReport/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, [FromQuery] DeleteType deleteType)
        {
            var success = await _service.DeleteAsync(id, deleteType);
            if (!success) return NotFound();
            return Ok();
        }

        // PATCH: api/InspectoraReport/restore/5
        [HttpPatch("restore/{id}")]
        public async Task<ActionResult> Restore(int id)
        {
            var success = await _service.RestoreLogical(id);
            if (!success) return NotFound();
            return Ok();
        }

        // GET: api/InspectoraReport/5/pdf
        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> DownloadContractPdf(int id)
        {
            var contract = await _service.GetByIdAsyncPdf(id);
            if (contract == null)
            {
                _logger.LogWarning("Contrato con ID {Id} no encontrado.", id);
                return NotFound(new { message = $"No se encontró un contrato con ID {id}" });
            }

            var pdfBytes = await _pdfService.GeneratePdfAsync(contract);

            return File(pdfBytes, "application/pdf", $"Contrato_{contract.Asunto}.pdf");
        }
    }
}