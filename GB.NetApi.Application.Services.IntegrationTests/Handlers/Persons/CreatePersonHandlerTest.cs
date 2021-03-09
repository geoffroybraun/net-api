using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.IntegrationTests.DataFixtures;
using GB.NetApi.Application.Services.IntegrationTests.ServicesFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.Persons
{
    public sealed class CreatePersonHandlerTest : IClassFixture<PersonDataFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private static readonly CreatePersonCommand Command = new CreatePersonCommand()
        {
            Birthdate = DateTime.UtcNow.AddHours(-1),
            Firstname = "New firstname",
            Lastname = "New lastname"
        };
        private readonly PersonDataFixture DataFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public CreatePersonHandlerTest(PersonDataFixture dataFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            DataFixture = dataFixture ?? throw new ArgumentNullException(nameof(dataFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public async Task Throwing_an_exception_let_it_be_thrown()
        {
            Task<bool> function()
            {
                var handler = new CreatePersonHandler(DataFixture.Broken, ServiceFixture.Broken);

                return handler.RunAsync(Command);
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_successfully_running_a_command_returns_false()
        {
            var handler = new CreatePersonHandler(DataFixture.Null, ServiceFixture.Null);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task Successfully_running_a_command_returns_true()
        {
            var handler = new CreatePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
