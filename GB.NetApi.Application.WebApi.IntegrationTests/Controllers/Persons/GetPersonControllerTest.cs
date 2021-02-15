using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Persons
{
    public sealed class GetPersonControllerTest : BasePersonControllerTest
    {
        #region Fields

        private static readonly string GetPersonEndpoint = $"{Endpoint}/{ID}";

        #endregion

        public GetPersonControllerTest(PersonDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Throwing_an_exception_when_getting_a_person_returns_an_internal_server_error_status_code()
        {
            var response = await GetAsync(BrokenClient, GetPersonEndpoint).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Not_finding_a_person_returns_a_not_found_status_code()
        {
            var result = await GetAsync(NullClient, GetPersonEndpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Successfully_finding_a_person_returns_the_expected_result()
        {
            var response = await GetAsync(Client, GetPersonEndpoint).ConfigureAwait(false);
            var result = await DeserializeContentAsync<PersonDto>(response.Content).ConfigureAwait(false);

            result.ID.Should().Be(ID);
        }
    }
}
