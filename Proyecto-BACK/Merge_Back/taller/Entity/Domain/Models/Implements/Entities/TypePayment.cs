using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Base;

namespace Entity.Domain.Models.Implements.Entities
{
    public class TypePayment : BaseModel
    {
        public string name { get; set; }
        public string description { get; set; }
        public int paymentAgreementId { get; set; }

        //relaciones
        public PaymentAgreement PaymentAgreement { get; set; }
    }
}
