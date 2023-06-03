using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Infrastructure.Configurations
{
    public class ArtikalPorudzbineConfiguration : IEntityTypeConfiguration<ArtikalPorudzbine>
    {
        public void Configure(EntityTypeBuilder<ArtikalPorudzbine> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Porudzbina)
                   .WithMany(x => x.ArtikliPorudzbine)
                   .HasForeignKey(x => x.PorudzbinaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
