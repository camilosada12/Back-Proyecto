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

        }
    }
}
