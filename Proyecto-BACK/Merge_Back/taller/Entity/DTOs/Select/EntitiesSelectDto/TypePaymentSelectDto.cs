using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Interfaces;
using Entity.Domain.Models.Base;

namespace Entity.Domain.Models.Implements.Entities
{
    public class TypePaymentSelectDto 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int paymentAgreementId { get; set; }
        public string paymentAgreementName { get; set; }
    }
}
