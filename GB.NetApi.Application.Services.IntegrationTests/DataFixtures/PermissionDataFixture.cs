using GB.NetApi.Domain.Models.Entities.Identity;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Infrastructure.Database.Repositories;
using Moq;
using System;

namespace GB.NetApi.Application.Services.IntegrationTests.DataFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="IPermissionRepository"/> implementation
    /// </summary>
    public sealed class PermissionDataFixture : BaseDataFixture<IPermissionRepository>
    {
        protected override Mock<IPermissionRepository> InitializeBrokenMock(Mock<IPermissionRepository> mock)
        {
            mock.Setup(m => m.ListAsync()).ThrowsAsync(new NotImplementedException());

            return mock;
        }

        protected override IPermissionRepository InitializeDummy() => new PermissionRepository(CommonReadableRepository);

        protected override Mock<IPermissionRepository> InitializeNullMock(Mock<IPermissionRepository> mock)
        {
            mock.Setup(m => m.ListAsync()).ReturnsAsync(default(Permission[]));

            return mock;
        }
    }
}
