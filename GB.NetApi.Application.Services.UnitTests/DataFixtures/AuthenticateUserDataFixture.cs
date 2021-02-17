using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Moq;
using System.Security.Claims;

namespace GB.NetApi.Application.Services.UnitTests.DataFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="IAuthenticateUserRepository"/> implementation
    /// </summary>
    public sealed class AuthenticateUserDataFixture : BaseDataFixture<IAuthenticateUserRepository>
    {
        #region Fields

        private static readonly AuthenticateUser User = new AuthenticateUser()
        {
            Claims = new[] { new Claim(typeof(string).Name, "value") },
            ID = "ID",
            Name = "Name",
            PermissionNames = new[] { "Permission" }
        };

        #endregion

        protected override Mock<IAuthenticateUserRepository> InitializeDummyMock(Mock<IAuthenticateUserRepository> mock)
        {
            mock.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(User);

            return mock;
        }
    }
}
