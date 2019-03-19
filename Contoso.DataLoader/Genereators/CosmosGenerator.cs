using Bogus;
using Contoso.DataAccess.Cosmos;
using Contoso.DataAccess.Cosmos.Interfaces;
using Contoso.DataAccess.Cosmos.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Contoso.DataLoader.Generators
{
    public class CosmosGenerator
    {
        public async Task GenerateDataAsync()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICosmosClientWrapper, CosmosClientWrapper>((sp) =>
            {
                return new CosmosClientWrapper("", "");
            });
            serviceCollection.AddScoped<IDataRepository<Customer, string>, DataRepository<Customer, string>>((sp) =>
            {
                var cosmosClientWrapper = sp.GetRequiredService<ICosmosClientWrapper>();

                // TODO: Is there a better way to do this? Would like to make delegate function above async / await.
                cosmosClientWrapper.CreateCollectionIfNotExists("ContosoOrders", "Customer", "/LastName").GetAwaiter().GetResult();
                return new DataRepository<Customer, string>(cosmosClientWrapper);
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var customerGenerator = new Faker<Customer>()
                .RuleFor(c => c.FirstName, p => p.Person.FirstName)
                .RuleFor(c => c.LastName, p => p.Person.LastName)
                .RuleFor(c => c.BirthDate, p => p.Person.DateOfBirth);

            var dataRepository = serviceProvider.GetRequiredService<IDataRepository<Customer, string>>();
            foreach (var fakeCustomer in customerGenerator.Generate(100))
            {
                await dataRepository.CreateItemAsync(fakeCustomer.LastName, fakeCustomer);
            }
        }
    }
}
