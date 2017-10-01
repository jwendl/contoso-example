﻿using ContosoExample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoExample.Business.Bootstrap
{
    public static class BusinessBootstrapper
    {
        public static void AddBusinessDependencies(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("ContosoExample"));
        }
    }
}
