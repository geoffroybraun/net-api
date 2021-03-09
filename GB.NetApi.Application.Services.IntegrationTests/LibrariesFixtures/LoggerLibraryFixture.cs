using GB.NetApi.Domain.Models.Enums;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Infrastructure.Libraries.Loggers;
using Moq;
using System;

namespace GB.NetApi.Application.Services.IntegrationTests.LibrariesFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="ILogger"/> implementation
    /// </summary>
    public sealed class LoggerLibraryFixture : BaseLibraryFixture<ILogger>
    {
        protected override Mock<ILogger> InitializeBrokenMock(Mock<ILogger> mock)
        {
            mock.Setup(m => m.Log(It.IsAny<ELogLevel>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new NotImplementedException());
            mock.Setup(m => m.Log(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new NotImplementedException());

            return mock;
        }

        protected override ILogger InitializeDummy() => new DebugLogger();

        protected override Mock<ILogger> InitializeNullMock(Mock<ILogger> mock)
        {
            mock.Setup(m => m.Log(It.IsAny<ELogLevel>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            mock.Setup(m => m.Log(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            return mock;
        }
    }
}
