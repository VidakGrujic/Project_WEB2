using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Infrastructure.Configurations
{
    public class AdministratorConfiguration : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.KorisnickoIme).HasMaxLength(50);
            builder.Property(x => x.Ime).HasMaxLength(30);
            builder.Property(x => x.Prezime).HasMaxLength(30);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
