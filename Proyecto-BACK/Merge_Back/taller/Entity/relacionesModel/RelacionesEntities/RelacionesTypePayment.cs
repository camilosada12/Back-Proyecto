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
    public class RelacionesTypePayment : IEntityTypeConfiguration<TypePayment>
    {
        public void Configure(EntityTypeBuilder<TypePayment> builder)
        {
            // Nombre de tabla
            builder.ToTable("typePayment", schema: "Entities");

            // Propiedades del baseModel
            builder.ConfigureBaseModel();

            // Índice único en name
            builder.HasIndex(tp => tp.name).IsUnique();
        }
    }
}
