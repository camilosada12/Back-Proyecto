using Business.Interfaces.IBusinessImplements.Entities;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Interface.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.ControllersBase.Web.Controllers.BaseController;

namespace Web.Controllers.Implements.Entities
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    [Produces("application/json")]
    public class UserInfractionController : BaseController<UserInfractionDto,UserInfractionSelectDto,IUserInfractionServices>
    {
        public UserInfractionController(IUserInfractionServices services, ILogger<UserInfractionController> logger) : base(services, logger)
        {
        }
        protected override Task<IEnumerable<UserInfractionSelectDto>> GetAllAsync(GetAllType getAllType) => _service.GetAllAsync(getAllType);
        protected override Task<UserInfractionSelectDto?> GetByIdAsync(int id) => _service.GetByIdAsync(id);
        protected override Task AddAsync(UserInfractionDto dto) => _service.CreateAsync(dto);
        protected override Task<bool> UpdateAsync(int id, UserInfractionDto dto) => _service.UpdateAsync(dto);

        protected override Task<bool> DeleteAsync(int id, DeleteType deleteType) => _service.DeleteAsync(id, deleteType);


        protected override Task<bool> RestaureAsync(int id) => _service.RestoreLogical(id);
    }
}
