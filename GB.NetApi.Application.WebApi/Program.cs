using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GB.NetApi.Application.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((builder) => builder.ClearProviders())
                .ConfigureWebHostDefaults((builder) => builder.UseStartup<Startup>())
                .Build()
                .Run();
        }
    }
}
