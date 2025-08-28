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
    // 2. TypeInfraction Configuration
    public class RelacionesTypeInfraction : IEntityTypeConfiguration<TypeInfraction>
    {
        public void Configure(EntityTypeBuilder<TypeInfraction> builder)
        {
            // Nombre de tabla
            builder.ToTable("typeInfraction", schema: "Entities");

            // Propiedades del baseModel
            builder.ConfigureBaseModel();

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

            builder.HasIndex(ti => ti.type_Infraction).IsUnique();
        }
    }
}
