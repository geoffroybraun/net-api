using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.IntegrationTests.DataFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.Persons
{
    public sealed class UpdatePersonHandlerTest : IClassFixture<PersonDataFixture>
    {
        #region Fields

        private static readonly UpdatePersonCommand Command = new UpdatePersonCommand()
        {
            Birthdate = DateTime.UtcNow.AddHours(-1),
            Firstname = "New firstname",
            ID = 1,
            Lastname = "New lastname"
        };
        private readonly PersonDataFixture Fixture;

        #endregion

        public UpdatePersonHandlerTest(PersonDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public async Task Throwing_an_exception_when_handling_a_command_let_it_be_thrown()
        {
            Task<bool> function()
            {
                var handler = new UpdatePersonHandler(Fixture.Broken);

                return handler.RunAsync(Command);
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_handling_a_command_returns_false()
        {
            var handler = new UpdatePersonHandler(Fixture.Null);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task Successfully_handling_a_command_returns_true()
        {
            var handler = new UpdatePersonHandler(Fixture.Dummy);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
