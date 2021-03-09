using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GB.NetApi.Application.WebApi.Extensions
{
    /// <summary>
    /// Extends a <see cref="IEndpointRouteBuilder"/> implementation
    /// </summary>
    public static class IEndpointRouteBuilderExtension
    {
        /// <summary>
        /// Configure all health checks and controllers endpoints
        /// </summary>
        /// <param name="builder">The extended <see cref="IEndpointRouteBuilder"/> implementation</param>
        public static void MapHealthChecksAndControllers(this IEndpointRouteBuilder builder)
        {
            builder.MapHealthChecks("/health/database", new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                Predicate = (check) => check.Tags.Contains("database"),
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status204NoContent,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                    [HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                }
            });

            builder.MapControllers();
        }
    }
}
