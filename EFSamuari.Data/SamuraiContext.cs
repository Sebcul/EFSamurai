using System;
using Microsoft.EntityFrameworkCore;
using EFSamurai.Domain;

namespace EFSamuari.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Server = (localdb)\\mssqllocaldb; Database = EfSamurai; Trusted_Connection = True; ");
        }
    }
}
