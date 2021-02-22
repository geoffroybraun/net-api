using GB.NetApi.Domain.Models.Entities.Identity;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Moq;
using System.Collections.Generic;

namespace GB.NetApi.Application.Services.UnitTests.DataFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="IPermissionRepository"/> implementation
    /// </summary>
    public sealed class PermissionDataFixture : BaseDataFixture<IPermissionRepository>
    {
        #region Fields

        private static readonly IEnumerable<Permission> Permissions = new[]
        {
            new Permission() { ID = 1, Name = "Permission", OperationID = 1, ResourceID = 1 }
        };

        #endregion

        protected override Mock<IPermissionRepository> InitializeDummyMock(Mock<IPermissionRepository> mock)
        {
            mock.Setup(m => m.ListAsync()).ReturnsAsync(Permissions);

            return mock;
        }
    }
}
