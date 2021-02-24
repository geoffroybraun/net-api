using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Handlers.AuthenticateUsers;
using GB.NetApi.Application.Services.IntegrationTests.DataFixtures;
using GB.NetApi.Application.Services.Queries.AuthenticateUsers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.AuthenticateUsers
{
    public sealed class GetSingleAuthenticateUserHandlerTest : IClassFixture<AuthenticateUserDataFixture>
    {
        #region Fields

        private static readonly GetSingleAuthenticateUserQuery Query = new GetSingleAuthenticateUserQuery() { UserName = "Reader", UserEmail = "reader@localhost.com" };
        private readonly AuthenticateUserDataFixture Fixture;

        #endregion

        public GetSingleAuthenticateUserHandlerTest(AuthenticateUserDataFixture fixture)
        {
            Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public async Task Throwing_an_exception_when_executing_a_query_let_it_be_thrown()
        {
            Task<AuthenticateUserDto> function()
            {
                var handler = new GetSingleAuthenticateUserHandler(Fixture.Broken);

                return handler.ExecuteAsync(Query);
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_getting_a_result_when_executing_a_query_returns_a_default_value()
        {
            var handler = new GetSingleAuthenticateUserHandler(Fixture.Null);
            var result = await handler.ExecuteAsync(Query).ConfigureAwait(false);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Successfully_executing_a_query_returns_the_expected_result()
        {
            var handler = new GetSingleAuthenticateUserHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(Query).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Claims.Should().NotBeNull().And.NotBeEmpty();
            result.PermissionNames.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}
