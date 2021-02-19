using FluentAssertions;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using GB.NetApi.Application.WebApi.Models;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Authentication
{
    public sealed class LoginAuthenticationControllerTest : BaseControllerTest<IAuthenticateUserRepository>, IClassFixture<AuthenticateUserDataFixture>
    {
        #region Fields

        private const string Endpoint = "login";
        private const string UserName = "Reader";
        private const string Password = "reader";
        private static readonly LoginRequest Request = new() { UserName = UserName, Password = Password };

        #endregion

        public LoginAuthenticationControllerTest(AuthenticateUserDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Throwing_an_exception_when_loging_in_returns_an_internal_server_error_status_code()
        {
            var result = await PostAsync(BrokenClient, Endpoint, Request).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Providing_an_empty_request_when_loging_in_returns_a_bad_request_status_code()
        {
            var result = await PostAsync(Client, Endpoint, new LoginRequest()).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(null, Password)]
        [InlineData("", Password)]
        [InlineData(" ", Password)]
        [InlineData(UserName, null)]
        [InlineData(UserName, "")]
        [InlineData(UserName, " ")]
        [InlineData("InvalidUserName", Password)]
        [InlineData(UserName, "InvalidPassword")]
        public async Task Providing_an_invalid_request_when_loging_in_returns_a_bad_request_status_code(string username, string password)
        {
            var request = new LoginRequest() { UserName = username, Password = password };
            var result = await PostAsync(Client, Endpoint, request).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Successfully_logging_in_returns_an_authentication_token()
        {
            var response = await PostAsync(Client, Endpoint, Request).ConfigureAwait(false);
            var result = await DeserializeContentAsync<LoginResponse>(response.Content).ConfigureAwait(false);

            result.Should().NotBeNull();
        }
    }
}
