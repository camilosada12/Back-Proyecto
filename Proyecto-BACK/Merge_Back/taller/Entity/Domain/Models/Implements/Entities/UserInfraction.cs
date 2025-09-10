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
        public DateTime dateInfraction { get; set; }
        public bool stateInfraction {  get; set; }
        public string observations { get; set; }
        public int UserId { get; set; }          // FK
        public User User { get; set; } = null!;  // Navegación
        // requerido

        public int typeInfractionId { get; set; }
        public TypeInfraction typeInfraction { get; set; } = null!;

        public int UserNotificationId { get; set; }
        public UserNotification UserNotification { get; set; } = null!;


        public List<PaymentAgreement> paymentAgreement { get; set; } = new();
    }

}
