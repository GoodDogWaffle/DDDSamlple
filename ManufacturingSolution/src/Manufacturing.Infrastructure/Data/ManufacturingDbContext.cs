// Infrastructure Layer: EF Core による DB 永続化を実装
using Microsoft.EntityFrameworkCore;
using Manufacturing.Domain.Entities.Sales;
using Manufacturing.Domain.Entities.Inventory;
using Manufacturing.Infrastructure.Data.Models;

namespace Manufacturing.Infrastructure.Data
{
    public class ManufacturingDbContext : DbContext
    {
        public ManufacturingDbContext(DbContextOptions<ManufacturingDbContext> opts)
            : base(opts) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Order>().HasKey(x => x.Id);
            mb.Entity<InventoryItem>().HasKey(x => x.Id);
            mb.Entity<Customer>().HasKey(x => x.Id);
        }
    }
}