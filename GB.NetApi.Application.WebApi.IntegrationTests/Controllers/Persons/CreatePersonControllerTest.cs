using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Persons
{
    public sealed class CreatePersonControllerTest : BasePersonControllerTest
    {
        #region Fields

        private static readonly CreatePersonCommand Command = new CreatePersonCommand()
        {
            Birthdate = DateTime.Now,
            Firstname = "New firstname",
            Lastname = "New lastname"
        };

        #endregion
        public CreatePersonControllerTest(PersonDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Throwing_an_exception_when_creating_a_person_returns_an_internal_server_error_status_code()
        {
            var result = await PutAsync(BrokenClient, Endpoint, Command).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Not_creating_a_person_returns_an_internal_server_error_status_code()
        {
            var result = await PutAsync(NullClient, Endpoint, Command).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Providing_an_invalid_person_to_create_returns_a_bad_request_status_code()
        {
            var result = await PutAsync(Client, Endpoint, new CreatePersonCommand()).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Successfully_creating_a_person_returns_a_no_content_status_code()
        {
            var result = await PutAsync(Client, Endpoint, Command).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
