using Bogus;
using Contoso.DataAccess.Cosmos;
using Contoso.DataAccess.Cosmos.Interfaces;
using Contoso.DataAccess.Cosmos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Contoso.DataLoader.Generators
{
    public class CosmosGenerator
    {
        private readonly IConfigurationRoot configurationRoot;

        public CosmosGenerator(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot;
        }

        public async Task GenerateDataAsync()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();
            serviceCollection.AddScoped<ICosmosClientWrapper, CosmosClientWrapper>((sp) =>
            {
                var options = sp.GetRequiredService<IOptions<CosmosOptions>>();
                var configuration = options.Value;
                return new CosmosClientWrapper(configuration.EndpointUri.AbsoluteUri, configuration.PrimaryKey);
            });
            serviceCollection.AddScoped<IDataRepository<Customer, string>, DataRepository<Customer, string>>((sp) =>
            {
                var cosmosClientWrapper = sp.GetRequiredService<ICosmosClientWrapper>();
                var options = sp.GetRequiredService<IOptions<CosmosOptions>>();
                var configuration = options.Value;

                // TODO: Is there a better way to do this? Would like to make delegate function above async / await.
                cosmosClientWrapper.CreateCollectionIfNotExists(configuration.Database, "Customer", "/LastName").GetAwaiter().GetResult();
                return new DataRepository<Customer, string>(cosmosClientWrapper);
            });
            serviceCollection.Configure<CosmosOptions>(options => configurationRoot.GetSection("cosmos").Bind(options));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var customerGenerator = new Faker<Customer>()
                .RuleFor(c => c.Id, p => p.Random.Guid().ToString())
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
