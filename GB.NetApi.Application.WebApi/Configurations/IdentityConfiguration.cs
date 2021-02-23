using GB.NetApi.Application.WebApi.Interfaces;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GB.NetApi.Application.WebApi.Configurations
{
    /// <summary>
    /// Configure identity for the application
    /// </summary>
    public sealed class IdentityConfiguration : IWebApiConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<UserDao>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>((options) =>
            {
                ConfigurePasswordOptions(options.Password);
                ConfigureLockoutOptions(options.Lockout);
            });
            services.ConfigureApplicationCookie((options) =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
            });
            services.AddScoped<IUserStore<UserDao>>((provider) =>
            {
                var contextFunction = provider.GetRequiredService<Func<BaseDbContext>>();

                return new UserStore<UserDao>(contextFunction(), new IdentityErrorDescriber());
            });
            services.AddScoped<IRoleStore<RoleDao>>((provider) =>
            {
                var contextFunction = provider.GetRequiredService<Func<BaseDbContext>>();

                return new RoleStore<RoleDao>(contextFunction(), new IdentityErrorDescriber());
            });
        }

        #region Private methods

        private static void ConfigurePasswordOptions(PasswordOptions options)
        {
            options.RequireDigit = true;
            options.RequireLowercase = true;
            options.RequireUppercase = true;
            options.RequiredLength = 8;
        }

        private static void ConfigureLockoutOptions(LockoutOptions options)
        {
            options.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.MaxFailedAccessAttempts = 3;
            options.AllowedForNewUsers = true;
        }

        #endregion
    }
}
