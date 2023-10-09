using System.Collections.Generic;
using System.Reflection.Emit;
using VirtualClients_API.Models;
using Microsoft.EntityFrameworkCore;
using VirtualClients_API.Models.ClasesEspeciales;

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
            modelBuilder.Entity<ClienteTotal>().ToSqlQuery("sp_InformacionTotal");
            modelBuilder.Entity<Cliente>().ToTable(nameof(Cliente));
            modelBuilder.Entity<Condicion>().ToTable(nameof(Condicion));
        }
    }
}
