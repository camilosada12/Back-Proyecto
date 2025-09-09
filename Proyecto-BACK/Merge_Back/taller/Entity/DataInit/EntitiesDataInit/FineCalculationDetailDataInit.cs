using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit.EntitiesDataInit
{
    public static class FineCalculationDetailDataInit
    {
        public static void SeedFineCalculationDetail(this ModelBuilder modelBuilder)
        {
            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<FineCalculationDetail>().HasData(
                new FineCalculationDetail
                {
                    id = 1,
                    formula = "salario minimo * dias = smdlv",
                    porcentaje = 0.5M,
                    totalCalculation = 100000M,
                    valueSmldvId = 1,
                    typeInfractionId = 1,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate,
                },
                 new FineCalculationDetail
                 {
                     id = 2,
                     formula = "salario minimo * dias = smdlv",
                     porcentaje = 0.0M,
                     totalCalculation = 150.000M,
                     valueSmldvId = 2,
                     typeInfractionId = 2,
                     active = true,
                     is_deleted = false,
                     created_date = seedDate,
                 }
                );
        }
    }
}
