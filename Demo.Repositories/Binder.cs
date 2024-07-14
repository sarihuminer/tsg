using Demo.Application.Interfaces.Repository;
using Demo.Domain.Model.Data;
using Demo.Repositories.Implementations.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repositories
{
    public static class Binder
    {
        public static IServiceCollection AddRepositoriesFactory(this IServiceCollection services)
        {
            // services.AddDbContext<DemoContex>();

            //add repositories and interfaces
            services.AddScoped(typeof(ICacheRepository<>), typeof(CachedRepository<>));
            return services;
            //services.AddTransient<IUnitOfWork>(provider=>
            //new UnitOfWork)
        }
    }
}
