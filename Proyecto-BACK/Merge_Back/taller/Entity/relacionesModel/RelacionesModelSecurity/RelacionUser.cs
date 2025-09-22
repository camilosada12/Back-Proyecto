// Entity.relacionesModel.RelacionesModelSecurity.RelacionUser
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.ModelSecurity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.relacionesModel.RelacionesModelSecurity
{
    public class RelacionUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", schema: "ModelSecurity");

            // ----- Campos básicos -----
            builder.Property(p => p.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(e => e.email)
                   .IsRequired(false)
                   .HasMaxLength(150)
                   .IsUnicode(false);

            // ----- Campos de verificación -----
            builder.Property(u => u.EmailVerified)
                   .HasDefaultValue(false);

            builder.Property(u => u.EmailVerificationCode)
                   .HasMaxLength(6)
                   .IsUnicode(false);

            builder.Property(u => u.EmailVerificationExpiresAt);
            builder.Property(u => u.EmailVerifiedAt);

            // 🔹 Estado del usuario
            builder.Property(u => u.Status)
                   .HasDefaultValue(UserStatus.Pending);

            // 🔹 Control de re-verificación mensual
            builder.Property(u => u.LastVerificationSentAt);
            builder.Property(u => u.LastReverificationAt);

            // ----- Relaciones -----
            builder.HasOne(u => u.Person)
                   .WithOne(p => p.User)
                   .HasForeignKey<User>(u => u.PersonId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_User_Person");

            builder.HasOne(p => p.documentType)
                   .WithMany(dt => dt.person)
                   .HasForeignKey(p => p.documentTypeId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
