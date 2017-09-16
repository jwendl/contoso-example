using ContosoExample.Data.Interfaces;
using ContosoExample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoExample.Data
{
    public class DataContext
        : DbContext, IDataContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
