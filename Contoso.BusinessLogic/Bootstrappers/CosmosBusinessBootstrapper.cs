using Contoso.DataAccess.Cosmos;
using Contoso.DataAccess.Cosmos.Interfaces;
using Contoso.DataAccess.Cosmos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Contoso.BusinessLogic.Bootstrappers
{
    public static class CosmosBusinessBootstrapper
    {
        public static void AddDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICosmosClientWrapper, CosmosClientWrapper>((sp) =>
            {
                var options = sp.GetRequiredService<IOptions<CosmosOptions>>();
                var option = options.Value;
                return new CosmosClientWrapper(option.EndpointUri.AbsoluteUri, option.PrimaryKey);
            });
            serviceCollection.AddScoped<IDataRepository<Customer, string>, DataRepository<Customer, string>>((sp) =>
            {
                var cosmosClientWrapper = sp.GetRequiredService<ICosmosClientWrapper>();
                var options = sp.GetRequiredService<IOptions<CosmosOptions>>();
                var option = options.Value;

                // TODO: Is there a better way to do this? Would like to make delegate function above async / await.
                cosmosClientWrapper.CreateCollectionIfNotExists(option.Database, "Customer", "/LastName").GetAwaiter().GetResult();
                return new DataRepository<Customer, string>(cosmosClientWrapper);
            });
        }
    }
}
