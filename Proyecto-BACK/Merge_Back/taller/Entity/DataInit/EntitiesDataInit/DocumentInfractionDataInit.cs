using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit.EntitiesDataInit
{
    public static class DocumentInfractionDataInit
    {
        public static void SeedDocumentInfraction(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentInfraction>().HasData(
                new DocumentInfraction
                {
                    id = 1,
                    inspectoraReportId = 1,
                    PaymentAgreementId = 1,
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                },
                 new DocumentInfraction
                 {
                     id = 2,
                     inspectoraReportId = 2,
                     PaymentAgreementId = 2,
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 02, 02),
                 }
                );
        }
    }
}
