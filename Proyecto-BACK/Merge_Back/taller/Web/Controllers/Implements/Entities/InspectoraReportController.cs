using Business.Interfaces.IBusinessImplements.Entities;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.ControllersBase.Web.Controllers.BaseController;



namespace Web.Controllers.Implements.Entities
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    [Produces("application/json")]
    public class InspectoraReportController : BaseController<InspectoraReportDto, InspectoraReportSelectDto, IInspectoraReportService>
    {
        public InspectoraReportController(IInspectoraReportService services,ILogger<InspectoraReportController> logger) : base(services, logger) 
        { 
        }
        protected override Task<IEnumerable<InspectoraReportSelectDto>> GetAllAsync(GetAllType getAllType) => _service.GetAllAsync(getAllType);
        protected override Task<InspectoraReportSelectDto?> GetByIdAsync(int id) => _service.GetByIdAsync(id);
        protected override Task AddAsync(InspectoraReportDto dto) => _service.CreateAsync(dto);
        protected override Task<bool> UpdateAsync(int id, InspectoraReportDto dto) => _service.UpdateAsync(dto);

        protected override Task<bool> DeleteAsync(int id, DeleteType deleteType) => _service.DeleteAsync(id, deleteType);


        protected override Task<bool> RestaureAsync(int id) => _service.RestoreLogical(id);
    }
}
