using GB.NetApi.Domain.Models.Entities.Identity;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Infrastructure.Database.Repositories;
using Moq;
using System;

namespace GB.NetApi.Application.Services.IntegrationTests.DataFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="IAuthenticateUserRepository"/> implementation
    /// </summary>
    public sealed class AuthenticateUserDataFixture : BaseDataFixture<IAuthenticateUserRepository>
    {
        protected override Mock<IAuthenticateUserRepository> InitializeBrokenMock(Mock<IAuthenticateUserRepository> mock)
        {
            mock.Setup(m => m.GetAsync(It.IsAny<string>())).ThrowsAsync(new NotImplementedException());

            return mock;
        }

        protected override IAuthenticateUserRepository InitializeDummy() => new AuthenticateUserRepository(CommonRepository);

        protected override Mock<IAuthenticateUserRepository> InitializeNullMock(Mock<IAuthenticateUserRepository> mock)
        {
            mock.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(default(AuthenticateUser));

            return mock;
        }
    }
}
