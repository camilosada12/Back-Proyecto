using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Interfaces;
using Entity.Domain.Models.Base;

namespace Entity.Domain.Models.Implements.Entities
{
    public class PaymentAgreementDto : IHasId
    {
        public int id { get; set; }
        public string address { get; set; }
        public string neighborhood { get; set; }
        public string financeAmount {  get; set; }
        public string AgreementDescription { get; set; }
        public int userInfractionId { get; set;}
        public int paymentFrequencyId { get; set; }
    }
}
