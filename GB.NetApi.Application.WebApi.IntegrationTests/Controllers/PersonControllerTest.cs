using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers
{
    public sealed class PersonControllerTest : BaseControllerTest
    {
        #region Fields

        private const string Endpoint = "persons";

        #endregion

        [Fact]
        public async Task Providing_an_invalid_person_to_create_returns_a_bad_request_status_code()
        {
            var result = await PutAsync(Endpoint, new CreatePersonCommand())
                .ConfigureAwait(false);

            result.StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Successfully_creating_a_person_returns_true()
        {
            var command = new CreatePersonCommand() { Birthdate = DateTime.Now, Firstname = "New firstname", Lastname = "New lastname" };
            var result = await PutAsync(Endpoint, command)
                .ConfigureAwait(false);

            result.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }
    }
}
