using Bogus;
using ContosoExample.Data;
using ContosoExample.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoExample.DataLoader
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(DataContext dataContext)
        {
            await dataContext.Database.EnsureCreatedAsync();

            if (dataContext.Customers.Any()) return;

            var locations = new Faker<Location>()
                .RuleFor(l => l.AddressLine1, p => p.Person.Address.Street)
                .RuleFor(l => l.City, p => p.Person.Address.City)
                .RuleFor(l => l.State, p => p.Address.State())
                .RuleFor(l => l.ZipCode, p => p.Address.ZipCode())
                .Generate(10)
                .ToList();
            locations.ForEach(async l => await dataContext.Locations.AddRangeAsync(l));
            await dataContext.SaveChangesAsync();

            var databaseLocations = await dataContext.Locations.ToListAsync();
            var customers = new Faker<Customer>()
                .RuleFor(c => c.FirstName, p => p.Person.FirstName)
                .RuleFor(c => c.LastName, p => p.Person.LastName)
                .RuleFor(c => c.BirthDate, p => p.Person.DateOfBirth)
                .RuleFor(c => c.UserAlias, p => p.Person.Email)
                .RuleFor(c => c.Location, p => p.PickRandom(databaseLocations))
                .Generate(10)
                .ToList();

            customers.ForEach(async c => await dataContext.Customers.AddRangeAsync(c));
            await dataContext.SaveChangesAsync();

            var databaseCustomers = await dataContext.Customers.ToListAsync();
            var orders = new Faker<Order>()
                .RuleFor(o => o.OrderNumber, p => p.Finance.Account())
                .RuleFor(o => o.Customer, p => p.PickRandom(databaseCustomers))
                .Generate(100)
                .ToList();

            orders.ForEach(async o => await dataContext.Orders.AddRangeAsync(o));
            await dataContext.SaveChangesAsync();
        }
    }
}
