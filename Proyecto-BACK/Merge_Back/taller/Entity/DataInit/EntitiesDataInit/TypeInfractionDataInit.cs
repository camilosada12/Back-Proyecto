using System;
using Entity.Domain.Models.Implements.Entities;
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
                    description = "lanzar basura en un lugar publico",
                    numer_smldv = 4, // 🔹 antes estaba en FineCalculationDetail
                    active = true,
                    is_deleted = false,
                    created_date = seedDate
                },
                new TypeInfraction
                {
                    id = 2,
                    type_Infraction = "infraccion de tipo dos",
                    description = "hacer mucho ruido en un sitio publico",
                    numer_smldv = 8, // 🔹 antes estaba en FineCalculationDetail
                    active = true,
                    is_deleted = false,
                    created_date = seedDate
                },
                new TypeInfraction
                {
                    id = 3,
                    type_Infraction = "infraccion de tipo Tres",
                    description = "Portar armas, elementos cortantes, punzantes, o sustancias peligrosas en áreas comunes o lugares abiertos al público.",
                    numer_smldv = 16, // 🔹 antes estaba en FineCalculationDetail
                    active = true,
                    is_deleted = false,
                    created_date = seedDate
                },
                new TypeInfraction
                {
                    id = 4,
                    type_Infraction = "infraccion de tipo Cuatro",
                    description = "Agresión a la autoridad: Agredir o lanzar objetos a las autoridades de policía.",
                    numer_smldv = 32, // 🔹 antes estaba en FineCalculationDetail
                    active = true,
                    is_deleted = false,
                    created_date = seedDate
                }
            );
        }
    }
}
