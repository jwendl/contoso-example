using Contoso.DataLoader.Generators;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Contoso.DataLoader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true);
            var configurationRoot = configurationBuilder.Build();

            var cosmosGenerator = new CosmosGenerator(configurationRoot);
            await cosmosGenerator.GenerateDataAsync();

            var entityFrameworkGenerator = new EntityFrameworkGenerator(configurationRoot);
            await entityFrameworkGenerator.GenerateDataAsync();
        }
    }
}
