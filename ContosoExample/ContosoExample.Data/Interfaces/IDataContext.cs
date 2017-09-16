using ContosoExample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoExample.Data.Interfaces
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbSet<Customer> Customers { get; set; }

        DbSet<Inventory> Inventories { get; set; }

        DbSet<Item> Items { get; set; }

        DbSet<Location> Locations { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<OrderItem> OrderItems { get; set; }
    }
}
