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

            // Campos
            builder.Property(p => p.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.email)
                   .IsRequired()
                   .HasMaxLength(150);

            // Índice único en email
            builder.HasIndex(u => u.email)
                   .IsUnique();

            // 1:1 OPCIONAL User <-> Person (sin cascada)
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
