using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Handlers.AuthenticateUsers;
using GB.NetApi.Application.Services.Queries.AuthenticateUsers;
using GB.NetApi.Application.Services.UnitTests.DataFixtures;
using GB.NetApi.Application.Services.UnitTests.ServicesFixtures;
using GB.NetApi.Domain.Models.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.AuthenticateUsers
{
    public sealed class GetSingleAuthenticateUserHandlerTest : IClassFixture<AuthenticateUserDataFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private const string UserEmail = "UserEmail";
        private readonly AuthenticateUserDataFixture DataFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public GetSingleAuthenticateUserHandlerTest(AuthenticateUserDataFixture dataFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            DataFixture = dataFixture ?? throw new ArgumentNullException(nameof(dataFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public void Providing_a_null_repository_in_constructor_throws_an_exception()
        {
            void action() => _ = new GetSingleAuthenticateUserHandler(null, ServiceFixture.Dummy);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public void Providing_a_null_translator_in_constructor_throws_an_exception()
        {
            void action() => _ = new GetSingleAuthenticateUserHandler(DataFixture.Dummy, null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_query_throws_an_exception()
        {
            Task<AuthenticateUserDto> function()
            {
                var handler = new GetSingleAuthenticateUserHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.ExecuteAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Providing_an_invalid_query_throws_an_exception(string userEmail)
        {
            Task<AuthenticateUserDto> function()
            {
                var handler = new GetSingleAuthenticateUserHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.ExecuteAsync(new GetSingleAuthenticateUserQuery() { UserEmail = userEmail });
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_an_invalid_query_returns_all_raised_error_messages_through_the_thrown_exception()
        {
            Task<AuthenticateUserDto> function()
            {
                var handler = new GetSingleAuthenticateUserHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.ExecuteAsync(new GetSingleAuthenticateUserQuery());
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Successfully_executing_a_query_returns_the_expected_result()
        {
            var handler = new GetSingleAuthenticateUserHandler(DataFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.ExecuteAsync(new GetSingleAuthenticateUserQuery() { UserEmail = UserEmail }).ConfigureAwait(false);

            result.Should().NotBeNull();
        }
    }
}
