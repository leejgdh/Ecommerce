using System;
using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace Ecormmerce.Models
{

    public class EcormmerceContext : DbContext
    {

        public EcormmerceContext(DbContextOptions options) : base(options)
        {

        }

        // public DbSet<Product> Products { get; set; }

        // public DbSet<Category> Categories { get; set; }

        public DbSet<Shop> Shops { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shop>()
            .ToContainer("Shops")
            .HasPartitionKey(nameof(Shop.BizNum));

            // modelBuilder.Entity<Category>()
            // .HasPartitionKey(nameof(Category.ParentId));

            // modelBuilder.Entity<Product>()
            // .HasPartitionKey(nameof(Product.CategoryId));
        }
    }
}