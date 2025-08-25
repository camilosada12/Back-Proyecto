using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Interfaces;
using Entity.Domain.Models.Base;
using Entity.Domain.Models.Implements.ModelSecurity;

namespace Entity.Domain.Models.Implements.Entities
{
    public class UserInfractionSelectDto
    {
        public int id { get; set; }
        public DateTime dateInfraction { get; set; } = DateTime.Now;
        public bool stateInfraction {  get; set; }
        public string observations { get; set; }
        public int userId { get; set; }
        public int typeInfractionId {get; set; }
        public int UserNotificationId { get; set; }
        public string userName { get; set; }
        public string typeInfractionName { get; set; }
        public string UserNotificationName { get; set; }
    }
}
