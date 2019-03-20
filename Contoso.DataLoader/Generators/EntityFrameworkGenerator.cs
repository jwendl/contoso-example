using Bogus;
using Contoso.DataAccess.EntityFramework;
using Contoso.DataAccess.EntityFramework.Interfaces;
using Contoso.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Contoso.DataLoader.Generators
{
    public class EntityFrameworkGenerator
    {
        private readonly IConfigurationRoot configurationRoot;

        public EntityFrameworkGenerator(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot;
        }

        public async Task GenerateDataAsync()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();
            serviceCollection.AddDbContext<DataContext>((sp, opt) =>
            {
                var options = sp.GetRequiredService<IOptions<EntityFrameworkOptions>>();
                var configuration = options.Value;
                opt.UseSqlServer(configuration.ConnectionString);
            });
            serviceCollection.AddScoped<IDataContext, DataContext>();
            serviceCollection.AddScoped(typeof(IDataRepository<,>), typeof(DataRepository<,>));
            serviceCollection.Configure<EntityFrameworkOptions>(options => configurationRoot.GetSection("entityFramework").Bind(options));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dataContext = serviceProvider.GetRequiredService<IDataContext>();
            await (dataContext as DbContext).Database.EnsureCreatedAsync();

            var dataRepository = serviceProvider.GetRequiredService<IDataRepository<Customer, int>>();
            var customerGenerator = new Faker<Customer>()
                .RuleFor(c => c.FirstName, p => p.Person.FirstName)
                .RuleFor(c => c.LastName, p => p.Person.LastName)
                .RuleFor(c => c.BirthDate, p => p.Person.DateOfBirth);

            var customers = customerGenerator.Generate(100);
            foreach (var customer in customers)
            {
                await dataRepository.CreateAsync(customer);
            }
        }
    }
}
