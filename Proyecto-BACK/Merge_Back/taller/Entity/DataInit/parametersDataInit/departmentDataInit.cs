using System;
using Entity.Domain.Models.Implements.parameters;
using Microsoft.EntityFrameworkCore;

namespace Entity.Data.Seeds.parameters
{
    public static class departmentDataInit
    {
        public static void SeedDepartment(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<department>().HasData(
                new department { id = 1, active = true, is_deleted = false, name = "Antioquia", daneCode = 5, created_date = new DateTime(2023, 01, 01) },
                new department { id = 2, active = true, is_deleted = false, name = "Cundinamarca", daneCode = 25, created_date = new DateTime(2023, 01, 01) },
                new department { id = 3, active = true, is_deleted = false, name = "Valle del Cauca", daneCode = 76, created_date = new DateTime(2023, 01, 01) },
                new department { id = 4, active = true, is_deleted = false, name = "Bogotá, D.C.", daneCode = 11, created_date = new DateTime(2023, 01, 01) }
            );
        }
    }
}
