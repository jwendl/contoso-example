using AutoMapper;
using Contoso.BusinessLogic.Bootstrappers.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.BusinessLogic.Bootstrappers
{
    public static class BusinessLogicBootstrapper
    {
        public static void AddDependencies(IServiceCollection serviceCollection)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
            });
            serviceCollection.AddSingleton(sp => mapperConfiguration.CreateMapper());
        }
    }
}
