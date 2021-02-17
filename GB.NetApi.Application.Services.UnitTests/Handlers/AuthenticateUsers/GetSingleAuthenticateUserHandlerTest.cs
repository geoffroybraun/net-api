using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Handlers.AuthenticateUsers;
using GB.NetApi.Application.Services.Queries.AuthenticateUsers;
using GB.NetApi.Application.Services.UnitTests.DataFixtures;
using GB.NetApi.Domain.Models.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.AuthenticateUsers
{
    public sealed class GetSingleAuthenticateUserHandlerTest : IClassFixture<AuthenticateUserDataFixture>
    {
        #region Fields

        private readonly AuthenticateUserDataFixture Fixture;

        #endregion

        public GetSingleAuthenticateUserHandlerTest(AuthenticateUserDataFixture fixture)
        {
            Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public void Providing_a_null_repository_in_constructor_throws_an_exception()
        {
            static void action() => _ = new GetSingleAuthenticateUserHandler(null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_query_to_execute_throws_an_exception()
        {
            Task<AuthenticateUserDto> function()
            {
                var handler = new GetSingleAuthenticateUserHandler(Fixture.Dummy);

                return handler.ExecuteAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Providing_an_invalid_query_to_execute_throws_an_exception(string userName)
        {
            Task<AuthenticateUserDto> function()
            {
                var handler = new GetSingleAuthenticateUserHandler(Fixture.Dummy);

                return handler.ExecuteAsync(new GetSingleAuthenticateUserQuery() { UserName = userName });
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Successfully_executing_a_query_returns_the_expected_result()
        {
            var handler = new GetSingleAuthenticateUserHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new GetSingleAuthenticateUserQuery() { UserName = "Name" }).ConfigureAwait(false);

            result.Should().NotBeNull();
        }
    }
}
