using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.UnitTests.DataFixtures;
using GB.NetApi.Application.Services.UnitTests.ServicesFixtures;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Persons
{
    public sealed class DeletePersonHandlerTest : IClassFixture<PersonDataFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private const int ID = 1;
        private readonly PersonDataFixture DataFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public DeletePersonHandlerTest(PersonDataFixture dataFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            DataFixture = dataFixture ?? throw new ArgumentNullException(nameof(dataFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public void Providing_a_null_repository_in_constructor_throws_an_exception()
        {
            void action() => _ = new DeletePersonHandler(null, ServiceFixture.Dummy);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public void Providing_a_null_translator_in_constructor_throws_an_exception()
        {
            void action() => _ = new DeletePersonHandler(DataFixture.Dummy, null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_command_throws_an_exception()
        {
            Task<bool> function()
            {
                var handler = new DeletePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.RunAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Providing_a_command_with_an_invalid_ID_throws_an_exception(int id)
        {
            Task<bool> function()
            {
                var handler = new DeletePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.RunAsync(new DeletePersonCommand() { ID = id });
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_command_with_an_inexisting_ID_throws_an_exception()
        {
            Task<bool> function()
            {
                var mock = new Mock<IPersonRepository>();
                mock.Setup(m => m.ExistAsync(It.IsAny<int>())).ReturnsAsync(false);
                var handler = new DeletePersonHandler(mock.Object, ServiceFixture.Dummy);

                return handler.RunAsync(new DeletePersonCommand() { ID = ID });
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_an_invalid_command_returns_all_raised_error_messages_through_the_thrown_exception()
        {
            Task<bool> function()
            {
                var handler = new DeletePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);

                return handler.RunAsync(new DeletePersonCommand());
            }
            var exception = await Assert.ThrowsAsync<EntityValidationException>(function).ConfigureAwait(false);

            exception.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Successfully_running_a_command_returns_true()
        {
            var handler = new DeletePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.RunAsync(new DeletePersonCommand() { ID = ID }).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
