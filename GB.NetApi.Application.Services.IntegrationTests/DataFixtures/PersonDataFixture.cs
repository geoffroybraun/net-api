using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Infrastructure.Database.Repositories;
using Moq;
using System;

namespace GB.NetApi.Application.Services.IntegrationTests.DataFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="IPersonRepository"/> implementation
    /// </summary>
    public sealed class PersonDataFixture : BaseDataFixture<IPersonRepository>
    {
        protected override Mock<IPersonRepository> InitializeBrokenMock(Mock<IPersonRepository> mock)
        {
            mock.Setup(m => m.CreateAsync(It.IsAny<Person>()))
                .ThrowsAsync(new NotImplementedException());
            mock.Setup(m => m.ExistAsync(It.IsAny<Person>()))
                .ThrowsAsync(new NotImplementedException());

            return mock;
        }

        protected override IPersonRepository InitializeDummy() => new PersonRepository(ContextFunction, TaskHandler);

        protected override Mock<IPersonRepository> InitializeNullMock(Mock<IPersonRepository> mock)
        {
            mock.Setup(m => m.CreateAsync(It.IsAny<Person>()))
                .ReturnsAsync(false);
            mock.Setup(m => m.ExistAsync(It.IsAny<Person>()))
                .ReturnsAsync(false);

            return mock;
        }
    }
}
