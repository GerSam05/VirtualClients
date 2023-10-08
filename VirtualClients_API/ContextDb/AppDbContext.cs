using System.Collections.Generic;
using System.Reflection.Emit;
using VirtualClients_API.Models;
using Microsoft.EntityFrameworkCore;

namespace VirtualClients_API.ContextDb
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Condicion> Condicions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable(nameof(Cliente));
            modelBuilder.Entity<Condicion>().ToTable(nameof(Condicion));
        }
    }
}
