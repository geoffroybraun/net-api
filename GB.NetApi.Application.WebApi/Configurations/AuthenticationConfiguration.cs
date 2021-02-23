using GB.NetApi.Application.WebApi.Interfaces;
using GB.NetApi.Application.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

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

            services.AddAuthentication((options) =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((options) =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authenticationConfiguration.Audience,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(authenticationConfiguration.Key)),
                    SaveSigninToken = false
                };
            });
        }
    }
}
