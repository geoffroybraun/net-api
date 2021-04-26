using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Persons
{
    public sealed class UpdatePersonControllerTest : BasePersonControllerTest
    {
        #region Fields

        private static readonly string UpdatePersonEndpoint = $"{Endpoint}/{ID}";
        private static readonly CreatePersonCommand Command = new()
        {
            Birthdate = DateTime.UtcNow,
            Firstname = "Updated firstname",
            Lastname = "Updated lastname"
        };

        #endregion

        public UpdatePersonControllerTest(PersonDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Updating_an_existing_person_without_being_authenticated_returns_an_unauthorized_status_code()
        {
            var result = await PutAsync(Client, UpdatePersonEndpoint, Command).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Updating_an_existing_person_without_being_allowed_to_returns_a_forbidden_status_code()
        {
            await AuthenticateAsync(Client, GuestRequest).ConfigureAwait(false);
            var result = await PutAsync(Client, UpdatePersonEndpoint, Command).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Throwing_an_exception_when_updating_an_existing_person_returns_an_internal_server_error_status_code()
        {
            await AuthenticateAsync(BrokenClient, WriterRequest).ConfigureAwait(false);
            var result = await PutAsync(BrokenClient, UpdatePersonEndpoint, Command).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Providing_a_null_person_to_update_returns_a_bad_request_status_code()
        {
            await AuthenticateAsync(Client, WriterRequest).ConfigureAwait(false);
            var result = await PutAsync<UpdatePersonCommand>(Client, UpdatePersonEndpoint, null).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Not_updating_an_existing_person_returns_an_internal_server_error_status_code()
        {
            await AuthenticateAsync(NullClient, WriterRequest).ConfigureAwait(false);
            var result = await PutAsync(NullClient, UpdatePersonEndpoint, Command).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Successfully_updating_an_existing_person_returns_a_no_content_status_code()
        {
            await AuthenticateAsync(Client, WriterRequest).ConfigureAwait(false);
            var result = await PutAsync(Client, UpdatePersonEndpoint, Command).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
