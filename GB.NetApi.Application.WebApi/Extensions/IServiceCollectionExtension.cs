using GB.NetApi.Application.WebApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GB.NetApi.Application.WebApi.Extensions
{
    /// <summary>
    /// Extends an <see cref="IServiceCollection"/> implementation
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Call all <see cref="IWebApiConfiguration"/> implementation within the assembly
        /// </summary>
        /// <param name="services">The extended <see cref="IServiceCollection"/> implementation</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> implementation</param>
        public static void ConfigureWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            var webApiConfigurations = typeof(Startup).Assembly
                .DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(IWebApiConfiguration)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IWebApiConfiguration>();

            foreach (var webApiConfiguration in webApiConfigurations)
                webApiConfiguration.Configure(services, configuration);
        }
    }
}
