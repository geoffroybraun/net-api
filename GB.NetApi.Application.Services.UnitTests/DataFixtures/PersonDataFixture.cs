using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Moq;

namespace GB.NetApi.Application.Services.UnitTests.DataFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="IPersonRepository"/> implementation
    /// </summary>
    public sealed class PersonDataFixture : BaseDataFixture<IPersonRepository>
    {
        protected override Mock<IPersonRepository> InitializeDummyMock(Mock<IPersonRepository> mock)
        {
            mock.Setup(m => m.CreateAsync(It.IsAny<Person>()))
                .ReturnsAsync(true);
            mock.Setup(m => m.ExistAsync(It.IsAny<Person>()))
                .ReturnsAsync(false);

            return mock;
        }
    }
}
