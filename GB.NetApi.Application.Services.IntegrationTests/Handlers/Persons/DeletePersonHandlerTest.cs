using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.IntegrationTests.DataFixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.Persons
{
    public sealed class DeletePersonHandlerTest : IClassFixture<PersonDataFixture>
    {
        #region Fields

        private static readonly DeletePersonCommand Command = new DeletePersonCommand() { ID = 1 };
        private readonly PersonDataFixture Fixture;

        #endregion

        public DeletePersonHandlerTest(PersonDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public async Task Throwing_an_exception_when_deleting_a_person_let_it_be_thrown()
        {
            Task<bool> function()
            {
                var handler = new DeletePersonHandler(Fixture.Broken);

                return handler.RunAsync(Command);
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_deleting_a_person_returns_false()
        {
            var handler = new DeletePersonHandler(Fixture.Null);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task Successfully_deleting_a_person_returns_true()
        {
            var handler = new DeletePersonHandler(Fixture.Dummy);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
