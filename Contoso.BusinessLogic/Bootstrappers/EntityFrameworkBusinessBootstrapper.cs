using Contoso.DataAccess.EntityFramework;
using Contoso.DataAccess.EntityFramework.Bootstrapper;
using Contoso.DataAccess.EntityFramework.Interfaces;
using Contoso.DataAccess.EntityFramework.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.BusinessLogic.Bootstrappers
{
    public class EntityFrameworkBusinessBootstrapper
    {
        public static void AddDependencies(IServiceCollection serviceCollection)
        {
            SqlServerBootstrapper.AddDependencies(serviceCollection);
            serviceCollection.AddScoped<IDataContext, DataContext>();
            serviceCollection.AddScoped(typeof(IDataRepository<,>), typeof(DataRepository<,>));
        }
    }
}
