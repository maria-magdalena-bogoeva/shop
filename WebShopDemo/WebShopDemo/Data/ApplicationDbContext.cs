using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebShopDemo.Domain;
using WebShopDemo.Models.Product;

namespace WebShopDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WebShopDemo.Models.Product.ProductCreateVM> ProductCreateVM { get; set; }
        public DbSet<WebShopDemo.Models.Product.ProductIndexVM> ProductIndexVM { get; set; }
        public DbSet<WebShopDemo.Models.Product.ProductEditVM> ProductEditVM { get; set; }
        public DbSet<WebShopDemo.Models.Product.ProductDetailsVM> ProductDetailsVM { get; set; }
        public DbSet<WebShopDemo.Models.Product.ProductDeleteVM> ProductDeleteVM { get; set; }
    }
}
