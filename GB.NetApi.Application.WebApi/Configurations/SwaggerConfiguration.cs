using GB.NetApi.Application.WebApi.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure a swagger endpoint for development environment only
    /// </summary>
    public sealed class SwaggerConfiguration : IWebApiConfiguration
    {
        #region Fields

        private const string AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;

        #endregion

        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen((options) =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Description = ".Net Api managing persons",
                    Title = ".Net Api",
                    Contact = new OpenApiContact()
                    {
                        Email = "admin@localhost.com",
                        Name = "Admin"
                    }
                });
                options.AddSecurityDefinition(AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Description = $"JWT Authorization header using the Bearer scheme. Example: \"{HeaderNames.Authorization}: {AuthenticationScheme} 'myToken'\"",
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = AuthenticationScheme
                            },
                            Scheme = OAuthDefaults.DisplayName,
                            Name = AuthenticationScheme,
                            In = ParameterLocation.Header,

                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
