using System;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit.EntitiesDataInit
{
    public static class InfractionDataInit
    {
        public static void SeddInfraction(this ModelBuilder modelBuilder)
        {
            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Infraction>().HasData(
                new Infraction
                {
                    id = 1,
                    TypeInfractionId = 1,
                    description = "lanzar basura en un lugar publico",
                    numer_smldv = 4,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate
                },
                new Infraction
                {
                    id = 2,
                    TypeInfractionId = 2,
                    description = "hacer mucho ruido en un sitio publico",
                    numer_smldv = 8,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate
                },
                new Infraction
                {
                    id = 3,
                    TypeInfractionId = 3,
                    description = "Portar armas, elementos cortantes, punzantes, o sustancias peligrosas en áreas comunes o lugares abiertos al público.",
                    numer_smldv = 16, 
                    active = true,
                    is_deleted = false,
                    created_date = seedDate
                },
                new Infraction
                {
                    id = 4,
                    TypeInfractionId = 4,
                    description = "Agresión a la autoridad: Agredir o lanzar objetos a las autoridades de policía.",
                    numer_smldv = 32, 
                    active = true,
                    is_deleted = false,
                    created_date = seedDate
                }
            );
        }
    }
}
