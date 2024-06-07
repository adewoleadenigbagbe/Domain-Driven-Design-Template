using App.Data.Entities;
using App.Data.Entities.Customers;
using App.Data.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Contexts
{
    public abstract class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string nameOrConnectionString): base(nameOrConnectionString)
        {
        }

        public ApplicationDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Orderline> Orderlines { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
