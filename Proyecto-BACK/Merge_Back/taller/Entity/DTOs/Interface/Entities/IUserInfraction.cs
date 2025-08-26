using System;
using Entity.Domain.Interfaces;

namespace Entity.DTOs.Interface.Entities
{
    public interface IUserInfraction : IHasId
    {
        DateTime dateInfraction { get; set; }
        bool stateInfraction { get; set; }
        string observations { get; set; }
        int userId { get; set; }
        int typeInfractionId { get; set; }
        int UserNotificationId { get; set; }
    }
}
