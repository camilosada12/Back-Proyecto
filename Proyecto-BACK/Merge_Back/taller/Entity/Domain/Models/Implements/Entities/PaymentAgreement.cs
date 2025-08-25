using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Base;
using Entity.Domain.Models.Implements.parameters;

namespace Entity.Domain.Models.Implements.Entities
{
    public class PaymentAgreement : BaseModel
    {
        public string address { get; set; }
        public string neighborhood { get; set; }
        public string AgreementDescription { get; set; }

        public int userInfractionId { get; set; }
        public UserInfraction userInfraction { get; set; }

        public int paymentFrequencyId { get; set; }
        public PaymentFrequency paymentFrequency { get; set; }

        public List<DocumentInfraction> documentInfraction { get; set; } = new List<DocumentInfraction>();
        public List<TypePayment> typePayments { get; set; } = new List<TypePayment>();
    }

}
