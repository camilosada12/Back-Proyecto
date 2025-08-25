using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entity.ConfigurationsBase;

namespace Entity.relacionesModel.RelacionesEntities
{
    // 1. PaymentAgreement Configuration
    public class RelacionesPaymentAgreement : IEntityTypeConfiguration<PaymentAgreement>
    {
        public void Configure(EntityTypeBuilder<PaymentAgreement> builder)
        {
            // Nombre de tabla
            builder.ToTable("paymentAgreement", schema: "Entities");

            // Propiedades del baseModel
            builder.ConfigureBaseModel();

            // Relación: PaymentAgreement -> UserInfraction (muchos a uno)
            builder.HasOne(pa => pa.userInfraction)
                   .WithMany(ui => ui.paymentAgreement)
                   .HasForeignKey(pa => pa.userInfractionId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_PaymentAgreement_UserInfraction");


            // Relación: PaymentAgreement -> TypePayment (uno a muchos)
            builder.HasMany(pa => pa.typePayments)
                   .WithOne(tp => tp.PaymentAgreement)
                   .HasForeignKey(tp => tp.paymentAgreementId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_TypePayment_PaymentAgreement");

            builder.HasOne(pa => pa.paymentFrequency)
           .WithMany(pf => pf.paymentAgreement)
           .HasForeignKey(pa => pa.paymentFrequencyId)
           .OnDelete(DeleteBehavior.Restrict)
           .IsRequired();

        }
    }
}
