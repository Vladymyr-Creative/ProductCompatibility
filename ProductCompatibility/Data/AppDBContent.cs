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
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Compatibility> Compatibility { get; set; }
        public DbSet<ProductsCompatibility> ProductsCompatibility { get; set; }
        public DbSet<ShopCartItem> ShopCartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";
            
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            modelBuilder.Entity<ProductsCompatibility>().HasAlternateKey(pc => new { pc.Product1Id, pc.Product2Id });
            modelBuilder.Entity<ProductsCompatibility>(pc =>
            {
                pc.HasOne(pc=>pc.Product1).WithMany().HasForeignKey(pc => pc.Product1Id).HasPrincipalKey(t => t.Id);
                pc.HasOne(pc => pc.Product2).WithMany().HasForeignKey(pc => pc.Product2Id).HasPrincipalKey(t => t.Id);
            });            
        }
    }
}
