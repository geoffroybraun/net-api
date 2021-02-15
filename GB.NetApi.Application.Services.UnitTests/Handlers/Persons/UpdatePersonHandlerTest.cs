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
    public sealed class UpdatePersonHandlerTest : IClassFixture<PersonDataFixture>
    {
        #region Fields

        private const int ID = 1;
        private const string Firstname = "New firstname";
        private const string Lastname = "New lastname";
        private static readonly DateTime Birthdate = DateTime.UtcNow;
        private readonly PersonDataFixture Fixture;

        #endregion

        public UpdatePersonHandlerTest(PersonDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public void Providing_a_null_repository_in_constructor_throws_an_exception()
        {
            static void action() => _ = new UpdatePersonHandler(null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_command_to_handle_throws_an_exception()
        {
            Task<bool> function()
            {
                var handler = new UpdatePersonHandler(Fixture.Dummy);

                return handler.RunAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_an_empty_command_to_handle_throws_an_exception()
        {
            Task<bool> function()
            {
                var handler = new UpdatePersonHandler(Fixture.Dummy);

                return handler.RunAsync(new UpdatePersonCommand());
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_valid_but_inexisting_command_ID_throws_an_exception()
        {
            Task<bool> function()
            {
                var mock = new Mock<IPersonRepository>();
                mock.Setup(m => m.ExistAsync(It.IsAny<int>())).ReturnsAsync(false);
                var handler = new UpdatePersonHandler(mock.Object);

                return handler.RunAsync(new UpdatePersonCommand() { Birthdate = Birthdate, Firstname = Firstname, ID = ID, Lastname = Lastname });
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_valid_but_existing_command_values_throws_an_exception()
        {
            Task<bool> function()
            {
                var mock = new Mock<IPersonRepository>();
                mock.Setup(m => m.ExistAsync(It.IsAny<int>())).ReturnsAsync(true);
                mock.Setup(m => m.ExistAsync(It.IsAny<Person>())).ReturnsAsync(true);
                var handler = new UpdatePersonHandler(mock.Object);

                return handler.RunAsync(new UpdatePersonCommand() { Birthdate = Birthdate, Firstname = Firstname, ID = ID, Lastname = Lastname });
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Successfully_handling_a_command_returns_true()
        {
            var handler = new UpdatePersonHandler(Fixture.Dummy);
            var command = new UpdatePersonCommand()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                ID = ID,
                Lastname = Lastname
            };
            var result = await handler.RunAsync(command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
