using Microsoft.EntityFrameworkCore;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Compatibility> Compatibility { get; set; }
        public DbSet<ProductsCompatibility> ProductsCompatibility { get; set; }
        public DbSet<ShopCartItem> ShopCartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductsCompatibility>().HasAlternateKey(pc => new { pc.Product1ID, pc.Product2ID });
            modelBuilder.Entity<ProductsCompatibility>(pc =>
            {
                pc.HasOne(pc=>pc.Product1).WithMany().HasForeignKey(pc => pc.Product1ID).HasPrincipalKey(t => t.ID);
                //pc.HasOne(pc => pc.Product2).WithMany().HasForeignKey(pc => pc.Product2ID).HasPrincipalKey(t => t.ID);
            });            
        }
    }
}
