using Business.Interfaces.IBusinessImplements.Entities;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.ControllersBase.Web.Controllers.BaseController;

namespace Web.Controllers.Implements.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TypeInfractionController : BaseController<TypeInfractionDto, TypeInfractionSelectDto, ITypeInfractionService>
    {
        public TypeInfractionController(ITypeInfractionService service, ILogger<TypeInfractionController> logger)
            : base(service, logger) { }

        protected override Task<IEnumerable<TypeInfractionSelectDto>> GetAllAsync(GetAllType getAllType) => _service.GetAllAsync(getAllType);
        protected override Task<TypeInfractionSelectDto?> GetByIdAsync(int id) => _service.GetByIdAsync(id);
        protected override Task AddAsync(TypeInfractionDto dto) => _service.CreateAsync(dto);
        protected override Task<bool> UpdateAsync(int id, TypeInfractionDto dto) => _service.UpdateAsync(dto);
        protected override Task<bool> DeleteAsync(int id, DeleteType deleteType) => _service.DeleteAsync(id, deleteType);
        protected override Task<bool> RestaureAsync(int id) => _service.RestoreLogical(id);


    }
}
