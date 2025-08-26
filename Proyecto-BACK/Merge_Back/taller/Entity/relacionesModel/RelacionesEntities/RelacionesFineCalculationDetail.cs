using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Domain.Models.Implements.Entities;
using Entity.ConfigurationsBase;

public class RelacionesFineCalculationDetail : IEntityTypeConfiguration<FineCalculationDetail>
{
    public void Configure(EntityTypeBuilder<FineCalculationDetail> builder)
    {
        builder.ToTable("FineCalculationDetail", "Entities");
        builder.ConfigureBaseModel();

        builder.Property(x => x.formula)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(x => x.porcentaje)
               .HasColumnType("decimal(5,2)")
               .IsRequired();

        builder.Property(x => x.totalCalculation)
               .HasColumnType("decimal(12,2)")
               .IsRequired();

        // Relación a TypeInfraction (sin sombras)
        builder.HasOne(x => x.typeInfraction)
               .WithMany(t => t.fineCalculationDetail)
               .HasForeignKey(x => x.typeInfractionId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_TypeInfraction_FineCalculationDetail");

        // Relación a ValueSmldv (sin sombras)
        builder.HasOne(x => x.valueSmldv)
               .WithMany(v => v.fineCalculationDetail)
               .HasForeignKey(x => x.valueSmldvId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_ValueSmldv_FineCalculationDetail");

        // 🔒 Evita que EF intente crear FKs sombra por convenciones
        builder.Ignore("TypeInfractionid");
        builder.Ignore("ValueSmldvid");
    }
}
