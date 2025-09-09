using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit.EntitiesDataInit
{
    public static class PaymentAgreementDataInit
    {
        public static void SeedPaymentAgreement(this ModelBuilder modelBuilder)
        {
            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<PaymentAgreement>().HasData(
            new PaymentAgreement
            {
                id = 1,
                address = "carrera 10",
                neighborhood = "eduardo santos",
                AgreementDescription = "se realizará a 4 cuotas de 200.000 los 15 de cada mes desde este momento",
                userInfractionId = 1,     // Debe existir en SeedUserInfraction
                paymentFrequencyId = 1,   // Debe existir en SeedPaymentFrequency
                active = true,
                is_deleted = false,
                created_date = seedDate
            },
            new PaymentAgreement
            {
                id = 2,
                address = "carrera 1",
                neighborhood = "panamá",
                AgreementDescription = "se realizará a 2 cuotas de 50.000 los 12 de cada mes desde este momento",
                userInfractionId = 2,
                paymentFrequencyId = 2,
                active = true,
                is_deleted = false,
                created_date = seedDate
            }
        );

        }
    }
}
