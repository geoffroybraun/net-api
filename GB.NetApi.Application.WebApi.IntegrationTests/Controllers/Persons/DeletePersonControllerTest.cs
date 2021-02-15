using FluentAssertions;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Persons
{
    public sealed class DeletePersonControllerTest : BasePersonControllerTest
    {
        #region Fields

        private static readonly string DeletePersonEndpoint = $"{Endpoint}/{ID}";

        #endregion

        public DeletePersonControllerTest(PersonDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Throwing_an_exception_when_deleting_a_person_returns_an_internal_server_error_status_code()
        {
            var result = await DeleteAsync(BrokenClient, DeletePersonEndpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Providing_an_invalid_person_ID_when_deleting_returns_a_bad_request_status_code()
        {
            var result = await DeleteAsync(Client, $"{Endpoint}/{0}").ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Not_deleting_a_person_using_its_ID_returns_an_internal_server_error()
        {
            var result = await DeleteAsync(NullClient, DeletePersonEndpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Successfully_deleting_a_person_using_its_ID_returns_a_no_content_status_content()
        {
            var result = await DeleteAsync(Client, DeletePersonEndpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
