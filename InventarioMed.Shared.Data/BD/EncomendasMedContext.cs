using InventarioMed.Shared.Data.Models;
using InventarioMed.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventarioMed.Shared.Data.BD
{
    public class EncomendasMedContext : IdentityDbContext<AccessUser, AccessRole, int>
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tag> Tags { get; set; }

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventarioMed_BD_V1;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação N:N entre Order e Tag
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Tags)
                .WithMany(t => t.Orders)
                .UsingEntity(j => j.ToTable("OrderTag"));
        }
    }
}
