using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Handlers.Permissions;
using GB.NetApi.Application.Services.IntegrationTests.DataFixtures;
using GB.NetApi.Application.Services.Queries.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.Permissions
{
    public sealed class ListPermissionsHandlerTest : IClassFixture<PermissionDataFixture>
    {
        #region Fields

        private readonly PermissionDataFixture Fixture;

        #endregion

        public ListPermissionsHandlerTest(PermissionDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public async Task Throwing_an_exception_when_listing_permissions_let_it_be_thrown()
        {
            Task<IEnumerable<PermissionDto>> function()
            {
                var handler = new ListPermissionsHandler(Fixture.Broken);

                return handler.ExecuteAsync(new ListPermissionsQuery());
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_listing_permissions_returns_a_adefault_value()
        {
            var handler = new ListPermissionsHandler(Fixture.Null);
            var result = await handler.ExecuteAsync(new ListPermissionsQuery()).ConfigureAwait(false);

            result.Should().BeNull();
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
