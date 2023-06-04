using Microsoft.EntityFrameworkCore;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Infrastructure
{
    public class ProdavnicaDbContext : DbContext
    {
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<ArtikalPorudzbine> ArtikliUPorudzbinama { get; set; }

        public ProdavnicaDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdavnicaDbContext).Assembly);
            
           
            

        }


    }
}
