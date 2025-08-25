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
    public static class ValueSmldvDataInit
    {
        public static void SeedValueSmldv(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ValueSmldv>().HasData(
                 new ValueSmldv
                 {
                     id = 1,
                     value_smldv = 43.500,
                     Current_Year = 2024,
                     minimunWage = 1300000m,
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 01, 01),
                 },
                new ValueSmldv
                {
                    id = 2,
                    value_smldv = 43.500,
                    Current_Year = 2022,
                    minimunWage = 1100000m,
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 02, 02),
                }
                );
        }
    }
}
