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
    public static class UserNotificationDataInit
    {
        public static void SeedUserNotificacion(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNotification>().HasData(
                 new UserNotification
                 {
                     id = 1,
                     message = "tienes una infraccion por favor acercate antes del 12 de marzo para sucdazanar tu multa o podria iniciar un cobro coativo luego del plazo",
                     shippingDate = new DateTime(2024,04,03),
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 01, 01),
                 },
                new UserNotification
                {
                    id = 2,
                    message = "tienes una infraccion por favor acercate antes del 12 de julio para sucdazanar tu multa o podria iniciar un cobro coativo luego del plazo",
                    shippingDate = new DateTime(2024, 06, 20),
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 02, 02),
                }
                );
        }
    }
}
