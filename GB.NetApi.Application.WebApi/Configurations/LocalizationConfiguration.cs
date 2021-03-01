using GB.NetApi.Application.WebApi.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure the translation mecanism based on the http entering request culture
    /// </summary>
    public sealed class LocalizationConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RequestLocalizationOptions>((config) =>
            {
                config.RequestCultureProviders.Clear();
                config.RequestCultureProviders.Add(new CustomRequestCultureProvider((context) =>
                {
                    var languages = context.Request.Headers[HeaderNames.AcceptLanguage].ToString();
                    var defaultLanguage = languages.Split(',').FirstOrDefault();
                    defaultLanguage = string.IsNullOrEmpty(defaultLanguage) ? "en" : defaultLanguage.Split('-')[0];

                    return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
                }));
            });
        }
    }
}
