using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }


        public DbSet<Product> products {  get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<DeliveryMethod> deliveryMethods { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }
    }
}
