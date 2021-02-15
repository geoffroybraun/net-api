using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.UnitTests.DataFixtures;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Persons
{
    public sealed class CreatePersonHandlerTest : IClassFixture<PersonDataFixture>
    {
        #region Fields

        private const string Firstname = "Firstname";
        private const string Lastname = "Lastname";
        private static readonly DateTime Birthdate = DateTime.Now;
        private readonly PersonDataFixture Fixture;

        #endregion

        public CreatePersonHandlerTest(PersonDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public void Providing_a_null_repository_implementation_in_constructor_throws_an_exception()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new CreatePersonHandler(null));

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_command_throws_an_exception()
        {
            var handler = new CreatePersonHandler(Fixture.Dummy);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.RunAsync(null)).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_an_invalid_command_throws_an_exception()
        {
            var handler = new CreatePersonHandler(Fixture.Dummy);
            var exception = await Assert.ThrowsAsync<EntityValidationException>(() => handler.RunAsync(new CreatePersonCommand())).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_valid_but_existing_command_values_throws_an_exception()
        {
            var mock = new Mock<IPersonRepository>();
            mock.Setup(m => m.ExistAsync(It.IsAny<Person>())).ReturnsAsync(true);
            var handler = new CreatePersonHandler(mock.Object);

            var command = new CreatePersonCommand()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                Lastname = Lastname
            };
            var exception = await Assert.ThrowsAsync<EntityValidationException>(() => handler.RunAsync(command)).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Successfully_handling_a_command_returns_true()
        {
            var handler = new CreatePersonHandler(Fixture.Dummy);
            var command = new CreatePersonCommand()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                Lastname = Lastname
            };
            var result = await handler.RunAsync(command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
