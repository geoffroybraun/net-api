using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Persons
{
    public sealed class ListPersonControllerTest : BasePersonControllerTest
    {
        public ListPersonControllerTest(PersonDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Listing_persons_without_being_authenticated_returns_an_unauthorized_status_code()
        {
            var result = await GetAsync(Client, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Listing_persons_without_being_allowed_to_returns_a_forbidden_status_code()
        {
            await AuthenticateAsync(Client, GuestRequest).ConfigureAwait(false);
            var response = await GetAsync(Client, Endpoint).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Throwing_an_exception_when_listing_persons_returns_an_internal_server_error_status_code()
        {
            await AuthenticateAsync(BrokenClient, ReaderRequest).ConfigureAwait(false);
            var result = await GetAsync(BrokenClient, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Not_listing_persons_returns_a_not_found_status_code()
        {
            await AuthenticateAsync(NullClient, ReaderRequest).ConfigureAwait(false);
            var result = await GetAsync(NullClient, Endpoint).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Successfully_listing_all_stored_persons_returns_the_expected_result()
        {
            await AuthenticateAsync(Client, ReaderRequest).ConfigureAwait(false);
            var response = await GetAsync(Client, Endpoint).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content);

            result.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}
