using GB.NetApi.Application.WebApi.Formatters;
using GB.NetApi.Application.WebApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure all required formatters
    /// </summary>
    public sealed class FormattersConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(options =>
            {
                options.InputFormatters.Clear();
                options.InputFormatters.Add(new JsonTextInputFormatter());

                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new JsonTextOutputFormatter());
            });
        }
    }
}
