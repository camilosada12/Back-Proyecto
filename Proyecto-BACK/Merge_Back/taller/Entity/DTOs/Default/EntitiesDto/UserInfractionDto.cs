using Entity.Domain.Interfaces;
using Entity.DTOs.Interface.Entities;

namespace Entity.Domain.Models.Implements.Entities
{
    public class UserInfractionDto : IHasId, IUserInfraction
    {
        public int id { get; set; }
        public DateTime dateInfraction { get; set; }
        public bool stateInfraction {  get; set; }
        public string observations { get; set; }
        public int userId { get; set; }
        public int typeInfractionId {get; set; }
        public int UserNotificationId { get; set; }

        public string? documentNumber { get; set; }

        public decimal amountToPay { get; set; }
    }
}
