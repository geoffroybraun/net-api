using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.NetApi.Application.WebApi.Interfaces
{
    /// <summary>
    /// Represents a configuration of the Web API
    /// </summary>
    public interface IWebApiConfiguration
    {
        /// <summary>
        /// Uses both provided <see cref="IServiceCollection"/> and <see cref="IConfiguration"/> implementation to configure the Web API
        /// </summary>
        /// <param name="services">The current <see cref="IServiceCollection"/> implementation</param>
        /// <param name="configuration">The current <see cref="IConfiguration"/> implementation</param>
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
