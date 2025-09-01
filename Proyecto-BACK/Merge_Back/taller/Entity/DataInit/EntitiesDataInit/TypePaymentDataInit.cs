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
    public static class TypePaymentDataInit
    {
        public static void SeedTypePayment(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypePayment>().HasData(
                 new TypePayment
                 {
                     id = 1,
                     name = "efectivo",
                     paymentAgreementId = 1,
                     active = true,
                     is_deleted = false,
                     created_date = new DateTime(2023, 01, 01),
                 },
                new TypePayment
                {
                    id = 2,
                    name = "nequi",
                    paymentAgreementId = 2,
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 02, 02),
                }
                );
        }
    }
}
