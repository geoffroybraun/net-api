using GB.NetApi.Application.WebApi.Interfaces;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.Interfaces;
using GB.NetApi.Infrastructure.Database.Repositories;
using GB.NetApi.Infrastructure.Database.Repositories.Commons;
using GB.NetApi.Infrastructure.Libraries.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure all required injected classes
    /// </summary>
    public sealed class InjectionConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureLibraries(services);
            ConfigureRepositories(services);
        }

        #region Private methods

        private static void ConfigureLibraries(IServiceCollection services)
        {
            services.AddScoped<ITaskHandler>((provider) => new TaskHandler());
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<Func<BaseDbContext>>((provider) => () => new InMemoryDbContext());
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<ICommonReadableRepository, CommonReadableRepository>();
            services.AddScoped<ICommonWritableRepository, CommonWritableRepository>();

            services.AddScoped<IAuthenticateUserRepository, AuthenticateUserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        #endregion
    }
}
