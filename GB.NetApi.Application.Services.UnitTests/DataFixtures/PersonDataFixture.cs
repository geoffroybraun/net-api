using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Entities.Filters;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;

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
            mock.Setup(m => m.FilterAsync(It.IsAny<PersonFilter>()))
                .ReturnsAsync(GetPersons);
            mock.Setup(m => m.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(GetPerson);
            mock.Setup(m => m.ListAsync())
                .ReturnsAsync(GetPersons);

            return mock;
        }

        #region Private methods

        private static IEnumerable<Person> GetPersons() => new[] { GetPerson() };

        private static Person GetPerson() => new Person() { ID = 1, Firstname = "Firstname", Lastname = "Lastname", Birthdate = DateTime.Now };

        #endregion
    }
}
