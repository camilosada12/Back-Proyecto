using Business.Interfaces.IBusinessImplements.Entities;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.ControllersBase.Web.Controllers.BaseController;

namespace Web.Controllers.Implements.Entities
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    [Produces("application/json")]
    public class PaymentAgreementController : BaseController<PaymentAgreementDto, PaymentAgreementSelectDto, IPaymentAgreementServices>
    {
        public PaymentAgreementController(IPaymentAgreementServices services, ILogger<PaymentAgreementController> logger) : base(services, logger)
        {
        }
        protected override Task<IEnumerable<PaymentAgreementSelectDto>> GetAllAsync(GetAllType getAllType) => _service.GetAllAsync(getAllType);
        protected override Task<PaymentAgreementSelectDto?> GetByIdAsync(int id) => _service.GetByIdAsync(id);
        protected override Task AddAsync(PaymentAgreementDto dto) => _service.CreateAsync(dto);
        protected override Task<bool> UpdateAsync(int id, PaymentAgreementDto dto) => _service.UpdateAsync(dto);

        protected override Task<bool> DeleteAsync(int id, DeleteType deleteType) => _service.DeleteAsync(id, deleteType);


        protected override Task<bool> RestaureAsync(int id) => _service.RestoreLogical(id);
    }
}
