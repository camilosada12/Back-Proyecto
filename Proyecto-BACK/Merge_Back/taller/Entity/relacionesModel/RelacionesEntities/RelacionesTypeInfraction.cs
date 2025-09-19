using System;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entity.ConfigurationsBase;

namespace Entity.relacionesModel.RelacionesEntities
{
    // 2. TypeInfraction Configuration
    public class RelacionesTypeInfraction : IEntityTypeConfiguration<TypeInfraction>
    {
        public void Configure(EntityTypeBuilder<TypeInfraction> builder)
        {
            // Nombre de tabla
            builder.ToTable("typeInfraction", schema: "Entities");

            // Propiedades base (id, fechas, active, is_deleted, etc.)
            builder.ConfigureBaseModel();

            // 🔹 numer_smldv (nuevo campo en TypeInfraction)
            builder.Property(ti => ti.numer_smldv)
                   .IsRequired(); // Cada tipo de infracción debe tener definido su valor en SMLDV

            // Relación: TypeInfraction -> UserInfraction (uno a muchos)
            //builder.HasMany(ti => ti.userInfractions)
            //       .WithOne(ui => ui.typeInfraction)
            //       .HasForeignKey(ui => ui.typeInfractionId)
            //       .OnDelete(DeleteBehavior.Restrict)
            //       .HasConstraintName("FK_TypeInfraction_UserInfraction");

            // Relación: TypeInfraction -> FineCalculationDetail (uno a muchos)
            builder.HasMany(ti => ti.fineCalculationDetail)
                   .WithOne(fcd => fcd.typeInfraction)
                   .HasForeignKey(fcd => fcd.typeInfractionId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_TypeInfraction_FineCalculationDetail");

            // 🔒 Garantiza que no se repitan nombres de infracción
            builder.HasIndex(ti => ti.type_Infraction).IsUnique();
        }
    }
}
