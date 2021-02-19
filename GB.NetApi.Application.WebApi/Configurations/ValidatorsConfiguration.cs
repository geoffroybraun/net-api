using FluentValidation.AspNetCore;
using GB.NetApi.Application.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure all required validators
    /// </summary>
    public sealed class ValidatorsConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>((options) => options.SuppressModelStateInvalidFilter = true);
            services.AddMvc().AddFluentValidation((configuration) =>
            {
                configuration.ValidatorOptions.LanguageManager.Enabled = false;
                configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
        }
    }
}
