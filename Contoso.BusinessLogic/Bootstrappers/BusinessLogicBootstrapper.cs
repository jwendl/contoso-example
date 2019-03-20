using Contoso.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.BusinessLogic.Bootstrappers
{
    public static class BusinessLogicBootstrapper
    {
        public static void AddDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDateTimeService, DateTimeService>();
            serviceCollection.AddScoped<ICustomerListService, CustomerListService>();
        }
    }
}
