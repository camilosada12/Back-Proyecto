// Entity.relacionesModel.RelacionesModelSecurity.RelacionUser
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

            // Campos básicos
            builder.Property(p => p.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // Si en tu modelo email es opcional, puedes dejarlo Required(false).
            // Como vas a verificar por email, normalmente es Requerido.
            builder.Property(e => e.email)
                   .IsRequired()
                   .HasMaxLength(150)
                   .IsUnicode(false);

            // Índice único en email
            builder.HasIndex(u => u.email)
                   .IsUnique();

            // ----- Campos de verificación de email -----

            // true/false con valor por defecto = false
            builder.Property(u => u.EmailVerified)
                   .HasDefaultValue(false);

            // Código (6 dígitos); no requerido (solo mientras esté pendiente de verificación)
            builder.Property(u => u.EmailVerificationCode)
                   .HasMaxLength(6)
                   .IsUnicode(false);

            // Fechas de expiración y verificado: nullables
            builder.Property(u => u.EmailVerificationExpiresAt);
            builder.Property(u => u.EmailVerifiedAt);

            // ----- Relaciones -----

            // 1:1 opcional User <-> Person (sin cascada)
            builder.HasOne(u => u.Person)
                   .WithOne(p => p.User)
                   .HasForeignKey<User>(u => u.PersonId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_User_Person");

            // FK opcional: documentType
            builder.HasOne(p => p.documentType)
                   .WithMany(dt => dt.person)
                   .HasForeignKey(p => p.documentTypeId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
