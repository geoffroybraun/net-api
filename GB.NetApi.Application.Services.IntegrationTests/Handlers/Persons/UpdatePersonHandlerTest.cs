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
    public sealed class UpdatePersonHandlerTest : IClassFixture<PersonDataFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private static readonly UpdatePersonCommand Command = new UpdatePersonCommand()
        {
            Birthdate = DateTime.UtcNow.AddHours(-1),
            Firstname = "New firstname",
            ID = 1,
            Lastname = "New lastname"
        };
        private readonly PersonDataFixture DataFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public UpdatePersonHandlerTest(PersonDataFixture dataFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            DataFixture = dataFixture ?? throw new ArgumentNullException(nameof(dataFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public async Task Throwing_an_exception_let_it_be_thrown()
        {
            Task<bool> function()
            {
                var handler = new UpdatePersonHandler(DataFixture.Broken, ServiceFixture.Broken);

                return handler.RunAsync(Command);
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_successfully_running_a_command_returns_false()
        {
            var handler = new UpdatePersonHandler(DataFixture.Null, ServiceFixture.Null);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task Successfully_running_a_command_returns_true()
        {
            var handler = new UpdatePersonHandler(DataFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
