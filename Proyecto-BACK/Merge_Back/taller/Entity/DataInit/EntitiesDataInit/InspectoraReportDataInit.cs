using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit.EntitiesDataInit
{
    public static class InspectoraReportDataInit
    {
        public static void SeetInspectoraReportData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InspectoraReport>().HasData(
                new InspectoraReport
                {
                    id = 1,
                    report_date = new DateTime(2023,02,02),
                    total_fines = 2,
                    message = "se integra una nueva multa",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                },
                 new InspectoraReport
                 {
                     id = 2,
                     report_date = new DateTime(2023, 03, 03),
                     total_fines = 3,
                     message = "se integra una nueva multa",
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 04, 04),
                 }
                );
        }
    }
}
