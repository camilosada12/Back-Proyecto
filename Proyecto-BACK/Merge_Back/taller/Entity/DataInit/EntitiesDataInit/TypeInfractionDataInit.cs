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
    public static class TypeInfractionDataInit
    {
       public static void SeddTypeInfraction(this ModelBuilder modelBuilder)
        {
            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<TypeInfraction>().HasData(
                 new TypeInfraction
                 {
                     id = 1,
                     type_Infraction = "infraccion de tipo uno",
                     numer_smldv = 2,
                     description = "lanzar basura en un lugar publico",
                     active = true,
                     is_deleted = false,
                     created_date = seedDate
                 },
                new TypeInfraction
                {
                    id = 2,
                    type_Infraction = "infraccion de tipo dos",
                    numer_smldv = 4,
                    description = "hacer mucho ruido en un sitio publico",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 02, 02),
                },
                 new TypeInfraction
                 {
                     id = 3,
                     type_Infraction = "infraccion de tipo Tres",
                     numer_smldv = 8,
                     description = "Portar armas, elementos cortantes, punzantes, o sustancias peligrosas en áreas comunes o lugares abiertos al público.",
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 02, 02),
                 },
                  new TypeInfraction
                  {
                      id = 4,
                      type_Infraction = "infraccion de tipo Cuatro",
                      numer_smldv = 16,
                      description = "Agresión a la autoridad: Agredir o lanzar objetos a las autoridades de policía. ",
                      active = true,
                      is_deleted = false,
                      created_date = new DateTime(2023, 02, 02),
                  }
                    created_date = seedDate,
                }
                );
        }
    }
}
