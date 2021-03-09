using GB.NetApi.Application.Services.Queries.AuthenticateUsers;
using GB.NetApi.Application.WebApi.Extensions;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.HealthChecks
{
    /// <summary>
    /// Represents a health check which indicates if a database is available
    /// </summary>
    public sealed class DatabaseAvailabilityHealthCheck : IHealthCheck
    {
        #region Fields

        private readonly IMediator Mediator;

        #endregion

        public DatabaseAvailabilityHealthCheck(IMediator mediator) => Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var user = await Mediator.ExecuteAsync(new GetSingleAuthenticateUserQuery() { UserEmail = "guest@localhost.com" }).ConfigureAwait(false);

            return user is not null ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
    }
}
