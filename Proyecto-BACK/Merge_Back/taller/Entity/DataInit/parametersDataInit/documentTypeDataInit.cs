using System;
using Entity.Domain.Models.Implements.parameters;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit.parametersDataInit
{
    public static class documentTypeDataInit
    {
        public static void SeedDocumentType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<documentType>().HasData(
                new documentType { id = 1, active = true, is_deleted = false, name = "Cédula de Ciudadanía", abbreviation = "CC", created_date = new DateTime(2023, 01, 01) },
                new documentType { id = 2, active = true, is_deleted = false, name = "Cédula de Extranjería", abbreviation = "CE", created_date = new DateTime(2023, 01, 01) },
                new documentType { id = 3, active = true, is_deleted = false, name = "Tarjeta de Identidad", abbreviation = "TI", created_date = new DateTime(2023, 01, 01) },
                new documentType { id = 4, active = true, is_deleted = false, name = "Pasaporte", abbreviation = "PAS", created_date = new DateTime(2023, 01, 01) }
            );
        }
    }
}
