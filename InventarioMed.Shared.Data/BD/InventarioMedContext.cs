using Microsoft.EntityFrameworkCore;
using InventarioMed.Shared.Entities;

namespace InventarioMed.Shared.Data.BD
{
    public class InventarioMedContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Tag> Tags { get; set; }

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventarioMed_BD_V1;Integrated Security=True;Encrypt=False;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Tags)
                .WithMany(t => t.Orders)
                .UsingEntity(j => j.ToTable("OrderTags"));
        }
    }
}
