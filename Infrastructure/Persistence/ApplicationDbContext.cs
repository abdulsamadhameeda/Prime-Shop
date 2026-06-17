using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext , IApplicationDbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
