using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Entities.Filters;
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
            mock.Setup(m => m.CreateAsync(It.IsAny<Person>())).ThrowsAsync(new NotImplementedException());
            mock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new NotImplementedException());
            mock.Setup(m => m.ExistAsync(It.IsAny<Person>())).ThrowsAsync(new NotImplementedException());
            mock.Setup(m => m.ExistAsync(It.IsAny<int>())).ThrowsAsync(new NotImplementedException());
            mock.Setup(m => m.FilterAsync(It.IsAny<PersonFilter>())).ThrowsAsync(new NotImplementedException());
            mock.Setup(m => m.GetAsync(It.IsAny<int>())).ThrowsAsync(new NotImplementedException());
            mock.Setup(m => m.ListAsync()).ThrowsAsync(new NotImplementedException());
            mock.Setup(m => m.UpdateAsync(It.IsAny<Person>())).ThrowsAsync(new NotImplementedException());

            return mock;
        }

        protected override IPersonRepository InitializeDummy() => new PersonRepository(CommonWritableRepository);

        protected override Mock<IPersonRepository> InitializeNullMock(Mock<IPersonRepository> mock)
        {
            mock.Setup(m => m.CreateAsync(It.IsAny<Person>())).ReturnsAsync(false);
            mock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);
            mock.Setup(m => m.ExistAsync(It.IsAny<Person>())).ReturnsAsync(false);
            mock.Setup(m => m.ExistAsync(It.IsAny<int>())).ReturnsAsync(true);
            mock.Setup(m => m.FilterAsync(It.IsAny<PersonFilter>())).ReturnsAsync(default(Person[]));
            mock.Setup(m => m.GetAsync(It.IsAny<int>())).ReturnsAsync(default(Person));
            mock.Setup(m => m.ListAsync()).ReturnsAsync(default(Person[]));
            mock.Setup(m => m.UpdateAsync(It.IsAny<Person>())).ReturnsAsync(false);

            return mock;
        }
    }
}
