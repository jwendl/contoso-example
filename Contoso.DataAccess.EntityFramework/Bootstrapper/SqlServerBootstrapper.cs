using Contoso.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Contoso.DataAccess.EntityFramework.Bootstrapper
{
    public static class SqlServerBootstrapper
    {
        public static void AddDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DataContext>((sp, opt) =>
            {
                var options = sp.GetRequiredService<IOptions<EntityFrameworkOptions>>();
                var configuration = options.Value;
                opt.UseSqlServer(configuration.ConnectionString);
            });
        }
    }
}
