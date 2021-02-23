using GB.NetApi.Application.WebApi.Filters;
using GB.NetApi.Application.WebApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure all required filters
    /// </summary>
    public sealed class FiltersConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc((options) =>
            {
                options.Filters.Add(new ExceptionFilter());
                options.Filters.Add(new ValidationFilter());
            });
        }
    }
}
