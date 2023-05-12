using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Infrastructure.Configurations
{
    public class PorudzbinaConfiguration : IEntityTypeConfiguration<Porudzbina>
    {
        public void Configure(EntityTypeBuilder<Porudzbina> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Komentar).HasMaxLength(5000);

            builder.HasOne(x => x.Korisnik)
                   .WithMany(x => x.Porudzbine)
                   .HasForeignKey(x => x.KorisnikId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Artikli)
                   .WithMany(x => x.Porudzbine);


        }
    }
}
