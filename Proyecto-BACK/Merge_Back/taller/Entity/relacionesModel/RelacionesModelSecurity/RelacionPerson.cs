using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.ConfigurationsBase;
using Entity.Domain.Models.Implements.ModelSecurity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.relacionesModel.RelacionesModelSecurity
{
    public class RelacionPerson : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person", schema: "ModelSecurity");
            builder.ConfigureBaseModel();

            builder.HasKey(p => p.id);

            builder.Property(p => p.firstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.lastName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.phoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(p => p.address).IsRequired().HasMaxLength(100);

            builder.HasOne(p => p.municipality)
                   .WithMany(m => m.person)          
                   .HasForeignKey(p => p.municipalityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.documentType)
                   .WithMany(dt => dt.person)
                   .HasForeignKey(p => p.documentTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.phoneNumber).IsUnique();
        }
    }

}
