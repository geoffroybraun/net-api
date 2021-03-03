using GB.NetApi.Application.WebApi.Interfaces;
using GB.NetApi.Application.WebApi.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// COnfigure the CORS policy for the application
    /// </summary>
    public sealed class CorsPolicyConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var corsConfiguration = new CorsConfiguration();
            configuration.Bind(nameof(CorsConfiguration), corsConfiguration);

            services.AddCors((options) =>
            {
                options.AddDefaultPolicy((policy) =>
                {
                    AddOrigin(policy, corsConfiguration.Origin);
                    policy.AllowAnyMethod();
                    policy.WithHeaders(corsConfiguration.Headers);

                    if (corsConfiguration.AllowCredentials)
                        policy.AllowCredentials();
                });
            });
        }

        #region Private methods

        private static void AddOrigin(CorsPolicyBuilder policy, string origin)
        {
            if (string.IsNullOrEmpty(origin))
            {
                policy.AllowAnyOrigin();

                return;
            }

            policy.WithOrigins(origin);
        }

        #endregion
    }
}
