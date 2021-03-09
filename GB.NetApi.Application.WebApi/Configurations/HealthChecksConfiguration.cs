using GB.NetApi.Application.WebApi.HealthChecks;
using GB.NetApi.Application.WebApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// COnfigure all available health cheacks
    /// </summary>
    public sealed class HealthChecksConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().AddCheck<DatabaseAvailabilityHealthCheck>(nameof(DatabaseAvailabilityHealthCheck), tags: new[] { "database" });
        }
    }
}
