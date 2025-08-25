using Business.Implements;
using Entity.Contexts;
using Entity.DTOs;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ExternalApiService _apiService;
        private readonly ExternalDbContext _context;

        public DataController(ExternalApiService apiService, ExternalDbContext context)
        {
            _apiService = apiService;
            _context = context;
        }

        [HttpGet("external")]
        public async Task<ActionResult<List<TouristicAttractionApiDto>>> GetExternalDataAsync()
        {
            var data = await _apiService.GetExternalDataAsync();
            return Ok(data);
        }

        [HttpGet("external-sin")]
        public ActionResult<List<TouristicAttractionApiDto>> GetExternalDataBlocking()
        {
            var data = _apiService.GetExternalData(); // <-- BLOQUEANTE
            return Ok(data);
        }


        //[HttpPost("import")]
        //public async Task<IActionResult> ImportDataAsync()
        //{
        //    var externalData = await _apiService.GetExternalDataAsync();

        //    var itemsToSave = externalData
        //        .Select(x => new ExternalItem { Name = x.Name, Description = x.Description })
        //        .ToList();

        //    _context.ExternalItems.AddRange(itemsToSave);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = $"{itemsToSave.Count} items imported." });
        //}

        [HttpPost("import")]
        public async Task<IActionResult> ImportBatchAsync([FromBody] List<ExternalItemDto> dtos)
        {
            var existingNames = await _context.ExternalItems
                .Where(x => dtos.Select(d => d.Name).Contains(x.Name))
                .Select(x => x.Name)
                .ToListAsync();

            var newItems = dtos
                .Where(d => !existingNames.Contains(d.Name))
                .Select(d => new ExternalItem
                {
                    Name = d.Name,
                    Description = d.Description
                })
                .ToList();

            if (newItems.Count == 0)
                return Conflict("Todas las atracciones ya existen.");

            _context.ExternalItems.AddRange(newItems);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Saved = newItems.Count,
                Skipped = existingNames
            });
        }



        [HttpGet]
        public async Task<ActionResult<List<ExternalItem>>> GetAllAsync()
        {
            return await _context.ExternalItems.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ExternalItemDto dto)
        {
            var existingItem = await _context.ExternalItems.FindAsync(id);
            if (existingItem == null)
                return NotFound($"No se encontró la atracción con ID {id}.");

            existingItem.Name = dto.Name;
            existingItem.Description = dto.Description;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Atracción actualizada exitosamente." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var item = await _context.ExternalItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.ExternalItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
