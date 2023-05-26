using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Infrastructure.Configurations
{
    public class ArtikalConfiguration : IEntityTypeConfiguration<Artikal>
    {
        public void Configure(EntityTypeBuilder<Artikal> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Id).HasMaxLength(50);
            builder.Property(x => x.Opis).HasMaxLength(5000);

            builder.HasOne(x => x.Prodavac)
                   .WithMany(x => x.ProdavceviArtikli)
                   .HasForeignKey(x => x.ProdavacId)
                   .OnDelete(DeleteBehavior.Cascade);
          
        }
    }
}
