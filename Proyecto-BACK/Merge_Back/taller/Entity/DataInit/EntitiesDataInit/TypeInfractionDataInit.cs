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
            modelBuilder.Entity<TypeInfraction>().HasData(
                 new TypeInfraction
                 {
                     id = 1,
                     type_Infraction = "infraccion de tipo uno",
                     numer_smldv = 2,
                     description = "lanzar basura en un lugar publico",
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 01, 01),
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
                }
                );
        }
    }
}
