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
            modelBuilder.Entity<FineCalculationDetail>().HasData(
                new FineCalculationDetail
                {
                    id = 1,
                    forumula = "salario minimo * dias = smdlv",
                    percentaje = 0.5,
                    totalCalculation = 168.000,
                    valueSmldvId = 1,
                    typeInfractionId = 1,
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                },
                 new FineCalculationDetail
                 {
                     id = 2,
                     forumula = "salario minimo * dias = smdlv",
                     percentaje = 0.0,
                     totalCalculation = 100.000,
                     valueSmldvId = 2,
                     typeInfractionId = 2,
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 02, 02),
                 }
                );
        }
    }
}
