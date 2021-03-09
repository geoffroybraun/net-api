using GB.NetApi.Application.Services.Translators;
using GB.NetApi.Application.WebApi.Interfaces;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Models.Interfaces.Services;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.Interfaces;
using GB.NetApi.Infrastructure.Database.Repositories;
using GB.NetApi.Infrastructure.Database.Repositories.Commons;
using GB.NetApi.Infrastructure.Libraries.Handlers;
using GB.NetApi.Infrastructure.Libraries.Loggers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            ConfigureServices(services);
            ConfigureLibraries(services);
            ConfigureRepositories(services);
        }

        #region Private methods

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITranslator>((provider) =>
            {
                var languageProvider = provider.GetRequiredService<IOptions<RequestLocalizationOptions>>();

                return new ResourceTranslator(languageProvider.Value.DefaultRequestCulture.Culture);
            });
        }

        private static void ConfigureLibraries(IServiceCollection services)
        {
            services.AddScoped<ITaskHandler>((provider) => new TaskHandler());
            services.AddSingleton<ILogger, NLogLogger>();
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
