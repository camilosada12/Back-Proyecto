using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit.EntitiesDataInit
{
    public static class UserInfractionDataInit
    {
        public static void SeedUserInfraction(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfraction>().HasData(
                 new UserInfraction
                 {
                     id = 1,
                     userId = 1,            
                     typeInfractionId = 1,    
                     UserNotificationId = 1, 
                     dateInfraction = new DateTime(2025,01,25),
                     stateInfraction = false,
                     observations = "la persona no opuso resistencia a la infraccion",
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 01, 01),
                 },
                new UserInfraction
                {
                    id = 2,
                    userId = 2,
                    typeInfractionId = 2,
                    UserNotificationId = 2,
                    dateInfraction = new DateTime(2025, 02, 15),
                    stateInfraction = false,
                    observations = "la persona se encontraba en estado de embriagues",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 02, 02),
                }
                );
        }
    }
}
