using GB.NetApi.Application.WebApi.Interfaces;
using GB.NetApi.Application.WebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure the authentication for the application
    /// </summary>
    public sealed class AuthenticationConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var authenticationConfiguration = new JwtTokenConfiguration();
            configuration.Bind(nameof(JwtTokenConfiguration), authenticationConfiguration);
            services.AddSingleton(authenticationConfiguration);
        }
    }
}
