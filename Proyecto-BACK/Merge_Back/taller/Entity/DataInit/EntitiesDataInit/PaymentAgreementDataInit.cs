using System;
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
                     expeditionCedula = new DateTime(2016, 01, 05, 0, 0, 0, DateTimeKind.Utc),
                     PhoneNumber = "3101234567",
                     Email = "user1@example.com",
                     AgreementStart = seedDate,
                     AgreementEnd = seedDate.AddMonths(4),
                     userInfractionId = 1,
                     paymentFrequencyId = 1,
                     typePaymentId = 1, // efectivo
                     active = true,
                     is_deleted = false,
                     created_date = seedDate,

                     // 🔹 Nuevos campos financieros
                     BaseAmount = 800000m,
                     AccruedInterest = 0m,
                     OutstandingAmount = 800000m,
                     IsPaid = false,
                     IsCoactive = false,
                     CoactiveActivatedOn = null,
                     LastInterestAppliedOn = null,

                     // 🔹 Nuevos campos de cuotas
                     Installments = 4,
                     MonthlyFee = 200000m
                 },
                 new PaymentAgreement
                 {
                     id = 2,
                     address = "carrera 1",
                     neighborhood = "panamá",
                     AgreementDescription = "se realizará a 2 cuotas de 50.000 los 12 de cada mes desde este momento",
                     expeditionCedula = new DateTime(2017, 01, 12, 0, 0, 0, DateTimeKind.Utc),
                     PhoneNumber = "3009876543",
                     Email = "user2@example.com",
                     AgreementStart = seedDate,
                     AgreementEnd = seedDate.AddMonths(2),
                     userInfractionId = 2,
                     paymentFrequencyId = 2,
                     typePaymentId = 2, // nequi
                     active = true,
                     is_deleted = false,
                     created_date = seedDate,

                     // 🔹 Nuevos campos financieros
                     BaseAmount = 100000m,
                     AccruedInterest = 0m,
                     OutstandingAmount = 100000m,
                     IsPaid = false,
                     IsCoactive = false,
                     CoactiveActivatedOn = null,
                     LastInterestAppliedOn = null,

                     // 🔹 Nuevos campos de cuotas
                     Installments = 2,
                     MonthlyFee = 50000m
                 }
             );

        }
    }
}
