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
    // 6. UserInfraction Configuration
    public class RelacionesUserInfraction : IEntityTypeConfiguration<UserInfraction>
    {
        public void Configure(EntityTypeBuilder<UserInfraction> builder)
        {
            builder.ToTable("userInfraction", schema: "Entities");
            builder.ConfigureBaseModel();

        }
    }
}

