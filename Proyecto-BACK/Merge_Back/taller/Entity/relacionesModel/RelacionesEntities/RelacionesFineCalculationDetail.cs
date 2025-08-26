using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.ConfigurationsBase;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.relacionesModel.RelacionesEntities
{
    public class RelacionesFineCalculationDetail : IEntityTypeConfiguration<FineCalculationDetail>
    {
        public void Configure(EntityTypeBuilder<FineCalculationDetail> builder)
        {
            // Configura la tabla y esquema
            builder.ToTable("FineCalculationDetail", schema: "Entities");

            // Propiedades del baseModel
            builder.ConfigureBaseModel();

            // Propiedades de clave foránea
            builder.Property(va => va.valueSmldvId).HasColumnName("valueSmldvId");
            builder.Property(ty => ty.typeInfractionId).HasColumnName("typeInfractionId");

            // Propiedad requerida
            builder.Property(i => i.is_deleted)
                   .IsRequired();

            builder.HasKey(x => x.id);

            builder.Property(x => x.forumula)
          .IsRequired()
          .HasMaxLength(200)
          .HasColumnType("varchar(200)");

            builder.Property(x => x.percentaje)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.totalCalculation)
                .IsRequired()
                .HasColumnType("decimal(12,2)");

            builder.HasOne(x => x.valueSmldv)
                .WithMany()
                .HasForeignKey(x => x.valueSmldvId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.typeInfraction)
                .WithMany()
                .HasForeignKey(x => x.typeInfractionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
