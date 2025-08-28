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

            builder.Property(x => x.observations)
                   .IsRequired();

            // Muchos UserInfraction -> Un User (INVERTIDA: User.UserInfraction)
            builder.HasOne(ui => ui.user)
            .WithMany(u => u.UserInfraction)
            .HasForeignKey(ui => ui.userId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_UserInfraction_User");

            // TypeInfraction (si tu entidad tiene colección, úsala; si no, WithMany() a secas)
            builder.HasOne(ui => ui.typeInfraction)
           .WithMany(ti => ti.userInfractions)       // <-- usa la colección real
           .HasForeignKey(ui => ui.typeInfractionId) // <-- una sola vez
           .OnDelete(DeleteBehavior.Restrict)
           .HasConstraintName("FK_TypeInfraction_UserInfraction");

            builder.HasOne(ui => ui.userNotification)
            .WithMany(un => un.userInfraction) // ✅ enlaza a la colección real
            .HasForeignKey(ui => ui.UserNotificationId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_UserNotification_UserInfraction");

        }
    }
}

