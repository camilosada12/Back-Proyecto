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
                    numer_smldv = 4,
                    //porcentaje = 0.5M,
                    totalCalculation = 0,
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
                    numer_smldv = 8,
                    //porcentaje = 0.0M,
                    totalCalculation = 0,
                    valueSmldvId = 1,
                    typeInfractionId = 2,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate,
                },
                new FineCalculationDetail
                {
                    id = 3,
                    formula = "salario minimo * dias = smdlv",
                    numer_smldv = 16,
                    //porcentaje = 0.0M,
                    totalCalculation = 0,
                    valueSmldvId = 1,
                    typeInfractionId = 3,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate,
                },
                new FineCalculationDetail
                {
                    id = 4,
                    formula = "salario minimo * dias = smdlv",
                    numer_smldv = 32,
                    //porcentaje = 0.2M,
                    totalCalculation = 0,
                    valueSmldvId = 1,
                    typeInfractionId = 4,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate,
                }
            );
        }
    }
}
