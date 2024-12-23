﻿using E_Commerce1DB_V01.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Type = E_Commerce1DB_V01.Entities.Type;

namespace E_Commerce1DB_V01
{
    public class ECPContext : DbContext
    {
        public ECPContext(DbContextOptions<ECPContext> options):base(options)
        {
            
        }
        public ECPContext()
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentLog> paymentLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
