using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.Services.Handlers.Logs;
using GB.NetApi.Application.Services.UnitTests.LibrariesFixtures;
using GB.NetApi.Application.Services.UnitTests.ServicesFixtures;
using GB.NetApi.Domain.Services.Extensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Logs
{
    public sealed class CreateBadRequestLogHandlerTest : IClassFixture<LoggerLibraryFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private const string ActionName = "ActionName";
        private const string ControllerName = "ControllerName";
        private const string Error = "Error";
        private readonly LoggerLibraryFixture LibraryFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public CreateBadRequestLogHandlerTest(LoggerLibraryFixture libraryFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            LibraryFixture = libraryFixture ?? throw new ArgumentNullException(nameof(libraryFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public void Providing_a_null_logger_in_constructor_throws_an_exception()
        {
            void action() => _ = new CreateBadRequestLogHandler(null, ServiceFixture.Dummy);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public void Providing_a_null_translator_in_constructor_throws_an_exception()
        {
            void action() => _ = new CreateBadRequestLogHandler(LibraryFixture.Dummy, null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_command_to_run_throws_an_exception()
        {
            Task<bool> function()
            {
                var handler = new CreateBadRequestLogHandler(LibraryFixture.Dummy, ServiceFixture.Dummy);

                return handler.RunAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null, ControllerName, Error)]
        [InlineData("", ControllerName, Error)]
        [InlineData(" ", ControllerName, Error)]
        [InlineData(ActionName, null, Error)]
        [InlineData(ActionName, "", Error)]
        [InlineData(ActionName, " ", Error)]
        [InlineData(ActionName, ControllerName, null)]
        [InlineData(ActionName, ControllerName, "")]
        [InlineData(ActionName, ControllerName, " ")]
        public async Task Providing_an_invalid_command_ro_tun_returns_false(string actionName, string controllerName, string error)
        {
            var command = new CreateBadRequestLogCommand()
            {
                ActionName = actionName,
                ControllerName = controllerName,
                Errors = error.IsNotNullNorEmptyNorWhiteSpace() ? new[] { error } : default
            };
            var handler = new CreateBadRequestLogHandler(LibraryFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.RunAsync(command).ConfigureAwait(false);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task Providing_a_valid_command_returns_true()
        {
            var command = new CreateBadRequestLogCommand()
            {
                ActionName = ActionName,
                ControllerName = ControllerName,
                Errors = new[] { Error }
            };
            var handler = new CreateBadRequestLogHandler(LibraryFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.RunAsync(command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
