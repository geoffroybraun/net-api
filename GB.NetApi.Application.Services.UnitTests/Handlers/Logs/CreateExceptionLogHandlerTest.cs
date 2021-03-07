using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.Services.Handlers.Logs;
using GB.NetApi.Application.Services.UnitTests.LibrariesFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Logs
{
    public sealed class CreateExceptionLogHandlerTest : IClassFixture<LoggerLibraryFixture>
    {
        #region Fields

        private readonly LoggerLibraryFixture Fixture;

        #endregion

        public CreateExceptionLogHandlerTest(LoggerLibraryFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public void Providing_a_null_logger_in_constructor_throws_an_exception()
        {
            static void action() => _ = new CreateExceptionLogHandler(null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_command_to_run_throws_an_exception()
        {
            Task<bool> function()
            {
                var handler = new CreateExceptionLogHandler(Fixture.Dummy);

                return handler.RunAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_an_invalid_command_to_run_returns_false()
        {
            var handler = new CreateExceptionLogHandler(Fixture.Dummy);
            var result = await handler.RunAsync(new CreateExceptionLogCommand()).ConfigureAwait(false);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task Successfully_running_a_command_returns_true()
        {
            var handler = new CreateExceptionLogHandler(Fixture.Dummy);
            var result = await handler.RunAsync(new CreateExceptionLogCommand() { Exception = new NotImplementedException() }).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
