using FluentAssertions;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.HealthChecks
{
    public sealed class HealthCheckTest : BaseControllerTest<IPersonRepository>, IClassFixture<PersonDataFixture>
    {
        #region Fields

        private const string Endpoint = "/health/database";

        #endregion

        public HealthCheckTest(PersonDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Checking_health_without_being_authentified_returns_an_unauthorized_status_code()
        {
            var result = await GetAsync(Client, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task Checking_health_without_being_being_allowed_to_returns_a_forbidden_status_code()
        {
            await AuthenticateAsync(Client, GuestRequest).ConfigureAwait(false);
            var result = await GetAsync(Client, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        }

        [Fact]
        public async Task Throwing_an_exception_returns_a_service_unavailable_status_code()
        {
            await AuthenticateAsync(BrokenClient, SuperviserRequest).ConfigureAwait(false);
            var result = await GetAsync(BrokenClient, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
        }

        [Fact]
        public async Task Not_scceussfully_checking_a_database_availability_still_returns_a_no_content_status_code_as_it_responded()
        {
            await AuthenticateAsync(NullClient, SuperviserRequest).ConfigureAwait(false);
            var result = await GetAsync(NullClient, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task Successfully_checking_a_database_availability_returns_a_no_content_status_code()
        {
            await AuthenticateAsync(Client, SuperviserRequest).ConfigureAwait(false);
            var result = await GetAsync(Client, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
