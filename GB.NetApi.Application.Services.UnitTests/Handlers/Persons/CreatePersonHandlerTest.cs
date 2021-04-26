using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.UnitTests.DataFixtures;
using GB.NetApi.Application.Services.UnitTests.ServicesFixtures;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Persons
{
    public sealed class CreatePersonHandlerTest : IClassFixture<PersonDataFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private const string Firstname = "Firstname";
        private const string Lastname = "Lastname";
        private static readonly DateTime Birthdate = DateTime.UtcNow.AddHours(-1);
        private readonly PersonDataFixture DataFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public CreatePersonHandlerTest(PersonDataFixture dataFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            DataFixture = dataFixture ?? throw new ArgumentNullException(nameof(dataFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public void Providing_a_null_repository_in_constructor_throws_an_exception()
        {
            void action() => _ = new CreatePersonHandler(null, ServiceFixture.Dummy);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public void Providing_a_null_translator_in_constructor_throws_an_exception()
        {
            void action() => _ = new CreatePersonHandler(DataFixture.Dummy, null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_command_throws_an_exception()
        {
            Task<bool> function()
            {
                var handler = new CreatePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.RunAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_an_invalid_command_throws_an_exception()
        {
            Task<bool> function()
            {
                var handler = new CreatePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.RunAsync(new CreatePersonCommand());
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_valid_but_existing_command_values_throws_an_exception()
        {
            Task<bool> function()
            {
                var mock = new Mock<IPersonRepository>();
                mock.Setup(m => m.ExistAsync(It.IsAny<Person>())).ReturnsAsync(true);
                var handler = new CreatePersonHandler(mock.Object, ServiceFixture.Dummy);

                var command = new CreatePersonCommand()
                {
                    Birthdate = Birthdate,
                    Firstname = Firstname,
                    Lastname = Lastname
                };

                return handler.RunAsync(command);
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_an_invalid_command_returns_all_raised_error_messages_through_the_thrown_exception()
        {
            Task<bool> function()
            {
                var handler = new CreatePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.RunAsync(new CreatePersonCommand());
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Errors.Count().Should().Be(3);
        }

        [Fact]
        public async Task Successfully_running_a_command_returns_true()
        {
            var handler = new CreatePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);
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
