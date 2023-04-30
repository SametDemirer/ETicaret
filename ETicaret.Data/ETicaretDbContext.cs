using ETicaret.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data
{
    public class ETicaretDbContext : DbContext
    {
        public ETicaretDbContext(DbContextOptions<ETicaretDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(x => x.Id);

            modelBuilder.Entity<Basket>()
                .HasOne(x => x.Order)
                .WithOne(x => x.Basket)
                .HasForeignKey<Order>(x => x.BasketId);

            //modelBuilder.Entity<BasketItem>()

        }

    }
}
