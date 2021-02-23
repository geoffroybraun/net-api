using GB.NetApi.Application.WebApi.Authorizations;
using GB.NetApi.Application.WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure the authorization process for the application
    /// </summary>
    public sealed class AuthorizationConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }
    }
}
