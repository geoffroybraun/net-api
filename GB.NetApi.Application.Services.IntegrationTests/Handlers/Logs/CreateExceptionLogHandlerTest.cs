using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.Services.Handlers.Logs;
using GB.NetApi.Application.Services.IntegrationTests.LibrariesFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.Logs
{
    public sealed class CreateExceptionLogHandlerTest : IClassFixture<LoggerLibraryFixture>
    {
        #region Fields

        private static readonly CreateExceptionLogCommand Command = new CreateExceptionLogCommand() { Exception = new TimeoutException() };
        private readonly LoggerLibraryFixture Fixture;

        #endregion

        public CreateExceptionLogHandlerTest(LoggerLibraryFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public async Task Throwing_an_exception_let_it_be_thrown()
        {
            Task<bool> function()
            {
                var handler = new CreateExceptionLogHandler(Fixture.Broken);

                return handler.RunAsync(Command);
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_successfully_running_a_command_still_return_true()
        {
            var handler = new CreateExceptionLogHandler(Fixture.Null);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task Successfully_running_a_command_returns_true()
        {
            var handler = new CreateExceptionLogHandler(Fixture.Dummy);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
