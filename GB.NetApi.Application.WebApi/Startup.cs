using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.WebApi.Extensions;
using GB.NetApi.Application.WebApi.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GB.NetApi.Application.WebApi
{
    public sealed class Startup
    {
        #region Fields

        private readonly IConfiguration Configuration;

        #endregion

        public Startup(IConfiguration configuration) => Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureWebApi(Configuration);
            services.AddMediatR(typeof(CreatePersonHandler));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler((options) => options.Run(ExceptionMiddleware.RequestDelegate));

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI((options) => options.SwaggerEndpoint("/swagger/v1/swagger.json", ".Net Api"));
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
