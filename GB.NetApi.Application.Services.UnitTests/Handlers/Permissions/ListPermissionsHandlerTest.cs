using FluentAssertions;
using GB.NetApi.Application.Services.Handlers.Permissions;
using GB.NetApi.Application.Services.Queries.Permissions;
using GB.NetApi.Application.Services.UnitTests.DataFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Permissions
{
    public sealed class ListPermissionsHandlerTest : IClassFixture<PermissionDataFixture>
    {
        #region Fields

        private readonly PermissionDataFixture Fixture;

        #endregion

        public ListPermissionsHandlerTest(PermissionDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public void Providing_a_null_repository_implementation_in_constructor_throws_an_exception()
        {
            static void action() => _ = new ListPermissionsHandler(null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Successfully_listing_permissions_returns_the_expected_result()
        {
            var handler = new ListPermissionsHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new ListPermissionsQuery()).ConfigureAwait(false);

            result.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}
