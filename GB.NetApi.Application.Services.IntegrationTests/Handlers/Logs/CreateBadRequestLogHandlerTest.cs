using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.Services.Handlers.Logs;
using GB.NetApi.Application.Services.IntegrationTests.LibrariesFixtures;
using GB.NetApi.Application.Services.IntegrationTests.ServicesFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.Logs
{
    public sealed class CreateBadRequestLogHandlerTest : IClassFixture<LoggerLibraryFixture>, IClassFixture<ResourceTranslatorServiceFixture>
    {
        #region Fields

        private static readonly CreateBadRequestLogCommand Command = new CreateBadRequestLogCommand()
        {
            ActionName = "ActionName",
            ControllerName = "ControllerName",
            Errors = new[] { "Error" }
        };
        private readonly LoggerLibraryFixture LibraryFixture;
        private readonly ResourceTranslatorServiceFixture ServiceFixture;

        #endregion

        public CreateBadRequestLogHandlerTest(LoggerLibraryFixture libraryFixture, ResourceTranslatorServiceFixture serviceFixture)
        {
            LibraryFixture = libraryFixture ?? throw new ArgumentNullException(nameof(libraryFixture));
            ServiceFixture = serviceFixture ?? throw new ArgumentNullException(nameof(serviceFixture));
        }

        [Fact]
        public async Task Throwing_an_exception_let_it_be_thrown()
        {
            Task<bool> function()
            {
                var handler = new CreateBadRequestLogHandler(LibraryFixture.Broken, ServiceFixture.Broken);

                return handler.RunAsync(Command);
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Null_successfully_running_a_command_still_returns_true()
        {
            var handler = new CreateBadRequestLogHandler(LibraryFixture.Null, ServiceFixture.Null);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task Successfully_running_a_command_returns_true()
        {
            var handler = new CreateBadRequestLogHandler(LibraryFixture.Dummy, ServiceFixture.Dummy);
            var result = await handler.RunAsync(Command).ConfigureAwait(false);

            result.Should().BeTrue();
        }
    }
}
