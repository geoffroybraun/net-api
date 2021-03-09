using FluentAssertions;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.HealthChecks
{
    public sealed class HealthCheckTest : BaseControllerTest<IAuthenticateUserRepository>, IClassFixture<AuthenticateUserDataFixture>
    {
        #region Fields

        private const string Endpoint = "/health/database";

        #endregion

        public HealthCheckTest(AuthenticateUserDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Throwing_an_exception_returns_an_service_unavailable_status_code()
        {
            var result = await GetAsync(BrokenClient, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
        }

        [Fact]
        public async Task Not_successfully_checking_the_database_availability_returns_a_service_unavailable_status_code()
        {
            var result = await GetAsync(NullClient, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
        }

        [Fact]
        public async Task Successfully_checking_a_database_availability_returns_a_no_content_status_code()
        {
            var result = await GetAsync(Client, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
