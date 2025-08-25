using Entity.Domain.Models.Implements.ModelSecurity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.relacionesModel.RelacionesModelSecurity
{
    public class RelacionUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tabla opcionalmente puedes nombrarla explícitamente
            builder.ToTable("user", schema: "ModelSecurity");

            //propiedad de name
            builder.Property(n => n.name)
                .IsRequired()
                .HasMaxLength(100);

            //propiedad de password
            builder.Property(p => p.password)
                .IsRequired()
                .HasMaxLength (100);

            //propiedad de email
            builder.Property(e => e.email)
                .IsRequired()
                .HasMaxLength (200);

            // Relación: User -> Person (muchos a uno)
            builder.HasOne(u => u.Person)
                   .WithOne(p => p.User)
                   .HasForeignKey<User>(u => u.PersonId)
                   .OnDelete(DeleteBehavior.Restrict) // Evita eliminación en cascada
                   .HasConstraintName("FK_User_Person");


            builder.HasIndex(u => u.email).IsUnique();
            builder.HasIndex(u => u.name).IsUnique();
        }
    }
}
