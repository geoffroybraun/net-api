using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Handlers.AuthenticateUsers;
using GB.NetApi.Application.Services.IntegrationTests.DataFixtures;
using GB.NetApi.Application.Services.IntegrationTests.ServicesFixtures;
using GB.NetApi.Application.Services.Queries.AuthenticateUsers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.AuthenticateUsers
{
    public sealed class GetSingleAuthenticateUserHandlerTest : IClassFixture<AuthenticateUserDataFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private static readonly GetSingleAuthenticateUserQuery Query = new GetSingleAuthenticateUserQuery() { UserEmail = "reader@localhost.com" };
        private readonly AuthenticateUserDataFixture DataFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public GetSingleAuthenticateUserHandlerTest(AuthenticateUserDataFixture dataFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            DataFixture = dataFixture ?? throw new ArgumentNullException(nameof(dataFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public async Task Throwing_an_exception_let_it_be_thrown()
        {
            Task<AuthenticateUserDto> function()
            {
                var handler = new GetSingleAuthenticateUserHandler(DataFixture.Broken, ServiceFixture.Broken);

                return handler.ExecuteAsync(Query);
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_successfully_executing_a_query_returns_a_default_value()
        {
            var handler = new GetSingleAuthenticateUserHandler(DataFixture.Null, ServiceFixture.Null);
            var result = await handler.ExecuteAsync(Query).ConfigureAwait(false);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Successfully_executing_a_query_returns_the_expected_result()
        {
            var handler = new GetSingleAuthenticateUserHandler(DataFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.ExecuteAsync(Query).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Claims.Should().NotBeNull().And.NotBeEmpty();
            result.PermissionNames.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}
