using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.Services.Handlers.Logs;
using GB.NetApi.Application.Services.UnitTests.LibrariesFixtures;
using GB.NetApi.Application.Services.UnitTests.ServicesFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Logs
{
    public sealed class CreateActionLogHandlerTest : IClassFixture<LoggerLibraryFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private const string ActionName = "ActionName";
        private const string ControllerName = "ControllerName";
        private const double ExecutionTime = 12;
        private readonly LoggerLibraryFixture LibraryFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public CreateActionLogHandlerTest(LoggerLibraryFixture libraryFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            LibraryFixture = libraryFixture ?? throw new ArgumentNullException(nameof(libraryFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public void Providing_a_null_logger_in_constructor_throws_an_exception()
        {
            void action() => _ = new CreateActionLogHandler(null, ServiceFixture.Dummy);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public void Providing_a_null_translator_in_constructor_throws_an_exception()
        {
            void action() => _ = new CreateActionLogHandler(LibraryFixture.Dummy, null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_command_to_run_throws_an_exception()
        {
            Task<bool> function()
            {
                var handler = new CreateActionLogHandler(LibraryFixture.Dummy, ServiceFixture.Dummy);

                return handler.RunAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null, ControllerName, ExecutionTime)]
        [InlineData("", ControllerName, ExecutionTime)]
        [InlineData(" ", ControllerName, ExecutionTime)]
        [InlineData(ActionName, null, ExecutionTime)]
        [InlineData(ActionName, "", ExecutionTime)]
        [InlineData(ActionName, " ", ExecutionTime)]
        [InlineData(ActionName, ControllerName, -1)]
        [InlineData(ActionName, ControllerName, 0)]
        public async Task Providing_an_invalid_command_returns_false(string actionName, string controllerName, double executionTime)
        {
            var command = new CreateActionLogCommand()
            {
                ActionName = actionName,
                ControllerName = controllerName,
                ExecutionTime = executionTime
            };
            var handler = new CreateActionLogHandler(LibraryFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.RunAsync(command).ConfigureAwait(false);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task Providing_a_valid_command_returns_true()
        {
            var command = new CreateActionLogCommand() { ActionName = ActionName, ControllerName = ControllerName, ExecutionTime = ExecutionTime };
            var handler = new CreateActionLogHandler(LibraryFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.RunAsync(command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
