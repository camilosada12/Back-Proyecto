using Data.Interfaces.DataBasic;
using Entity.Domain.Models.Implements.Entities;
using Entity.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IDataImplement.Entities
{
    public interface IPaymentAgreementRepository : IData<PaymentAgreement>
    {
        Task<IEnumerable<PaymentAgreementInitDto>> GetInitDataAsync(int userInfractionId);
    }
}
