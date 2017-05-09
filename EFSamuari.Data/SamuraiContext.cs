using System;
using Microsoft.EntityFrameworkCore;
using EFSamurai.Domain;

namespace EFSamuari.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<SamuraiBattles> SamuraiBattles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Server = (localdb)\\mssqllocaldb; Database = EfSamurai; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattles>().HasKey(t => new {t.SamuraiId, t.BattleId});
            modelBuilder.Entity<Samurai>().HasKey(x => x.Id);
        }
    }
}
