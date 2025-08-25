using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Base;
using Entity.Domain.Models.Implements.ModelSecurity;

namespace Entity.Domain.Models.Implements.Entities
{
    public class UserInfraction : BaseModel
    {
        public DateTime dateInfraction { get; set; } = DateTime.Now;
        public bool stateInfraction {  get; set; }
        public string observations { get; set; }
        public int userId { get; set; }
        public int typeInfractionId {get; set; }
        public int UserNotificationId { get; set; }

        //Relaciones
        public User user { get; set; }
        public List<PaymentAgreement> paymentAgreement { get; set; } = new List<PaymentAgreement>();
        public UserNotification userNotification { get; set; }
        public TypeInfraction typeInfraction { get; set; }
    }
}
