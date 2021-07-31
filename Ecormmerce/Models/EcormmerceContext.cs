using System;
using System.Net;
using Helper.Enums;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Converters;

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

        public DbSet<Product> Products { get; set; }


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


            modelBuilder.Entity<Product>()
            .ToContainer("Products")
            .HasPartitionKey(nameof(Product.ShopId));

            modelBuilder.Entity<Product>()
            .Property(e => e.Currency)
            .HasConversion(new EnumToStringConverter<ECurrency>())
            .HasMaxLength(5);


            modelBuilder.Entity<Product>()
            .Property(e => e.VariationType)
            .HasConversion(new EnumToStringConverter<EVariationType>())
            .HasMaxLength(20);


            modelBuilder.Entity<Product>()
            .Property(e => e.CategoryId)
            .HasConversion(new GuidToStringConverter());

            modelBuilder.Entity<Product>()
            .Property(e => e.ShopId)
            .HasConversion(new GuidToStringConverter());

            modelBuilder.Entity<Product>()
            .OwnsOne(e => e.Dimention);

            modelBuilder.Entity<Product>()
            .OwnsMany(e => e.Variations)
            .OwnsMany(e => e.ImageUrls);

            // modelBuilder.Entity<Category>()
            // .HasPartitionKey(nameof(Category.ParentId));

            // modelBuilder.Entity<Product>()
            // .HasPartitionKey(nameof(Product.CategoryId));
        }
    }
}