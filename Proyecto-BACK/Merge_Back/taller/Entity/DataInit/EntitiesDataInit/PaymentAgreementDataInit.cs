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

            // Simulación: SMDLV actual
            decimal smldv = 43500m;

            // Infracción 1: numer_smldv = 2, porcentaje = 0.5 → (43.500 * 2) * 1.5 = 130.500
            decimal baseAmount1 = Math.Round(smldv * 2 * 1.5m, 0);

            // Infracción 2: numer_smldv = 4, porcentaje = 0.0 → (43.500 * 4) = 174.000
            decimal baseAmount2 = Math.Round(smldv * 4 * 1.0m, 0);

            // Infracción 3: numer_smldv = 8, porcentaje = 0.0 → (43.500 * 8) = 348.000
            decimal baseAmount3 = Math.Round(smldv * 8 * 1.0m, 0);

            // Infracción 4: numer_smldv = 16, porcentaje = 0.2 → (43.500 * 16) * 1.2 = 835.200
            decimal baseAmount4 = Math.Round(smldv * 16 * 1.2m, 0);

            modelBuilder.Entity<PaymentAgreement>().HasData(
                new PaymentAgreement
                {
                    id = 1,
                    address = "carrera 10",
                    neighborhood = "eduardo santos",
                    AgreementDescription = "se realizará a 4 cuotas iguales",
                    expeditionCedula = new DateTime(2016, 01, 05, 0, 0, 0, DateTimeKind.Utc),
                    PhoneNumber = "3101234567",
                    Email = "user1@example.com",
                    AgreementStart = seedDate,
                    AgreementEnd = seedDate.AddMonths(4),
                    userInfractionId = 1,
                    paymentFrequencyId = 1,
                    typePaymentId = 1,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate,

                    BaseAmount = baseAmount1,
                    AccruedInterest = 0m,
                    OutstandingAmount = baseAmount1,
                    IsPaid = false,
                    IsCoactive = false,

                    Installments = 4,
                    MonthlyFee = baseAmount1 / 4
                },
                new PaymentAgreement
                {
                    id = 2,
                    address = "carrera 1",
                    neighborhood = "panamá",
                    AgreementDescription = "se realizará a 2 cuotas iguales",
                    expeditionCedula = new DateTime(2017, 01, 12, 0, 0, 0, DateTimeKind.Utc),
                    PhoneNumber = "3009876543",
                    Email = "user2@example.com",
                    AgreementStart = seedDate,
                    AgreementEnd = seedDate.AddMonths(2),
                    userInfractionId = 2,
                    paymentFrequencyId = 2,
                    typePaymentId = 2,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate,

                    BaseAmount = baseAmount2,
                    AccruedInterest = 0m,
                    OutstandingAmount = baseAmount2,
                    IsPaid = false,
                    IsCoactive = false,

                    Installments = 2,
                    MonthlyFee = baseAmount2 / 2
                },
                new PaymentAgreement
                {
                    id = 3,
                    address = "calle 20 #15-40",
                    neighborhood = "la merced",
                    AgreementDescription = "se realizará a 8 cuotas iguales",
                    expeditionCedula = new DateTime(2018, 03, 10, 0, 0, 0, DateTimeKind.Utc),
                    PhoneNumber = "3015558888",
                    Email = "user3@example.com",
                    AgreementStart = seedDate,
                    AgreementEnd = seedDate.AddMonths(8),
                    userInfractionId = 3,
                    paymentFrequencyId = 2,
                    typePaymentId = 3,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate,

                    BaseAmount = baseAmount3,
                    AccruedInterest = 0m,
                    OutstandingAmount = baseAmount3,
                    IsPaid = false,
                    IsCoactive = false,

                    Installments = 8,
                    MonthlyFee = baseAmount3 / 8
                },
                new PaymentAgreement
                {
                    id = 4,
                    address = "avenida 5 #45-12",
                    neighborhood = "san martin",
                    AgreementDescription = "se realizará a 12 cuotas iguales",
                    expeditionCedula = new DateTime(2019, 05, 22, 0, 0, 0, DateTimeKind.Utc),
                    PhoneNumber = "3024449999",
                    Email = "user4@example.com",
                    AgreementStart = seedDate,
                    AgreementEnd = seedDate.AddMonths(12),
                    userInfractionId = 4,
                    paymentFrequencyId = 3,
                    typePaymentId = 1,
                    active = true,
                    is_deleted = false,
                    created_date = seedDate,

                    BaseAmount = baseAmount4,
                    AccruedInterest = 0m,
                    OutstandingAmount = baseAmount4,
                    IsPaid = false,
                    IsCoactive = false,

                    Installments = 12,
                    MonthlyFee = baseAmount4 / 12
                }
            );
        }
    }
}
