using GB.NetApi.Domain.Models.Enums;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using Moq;
using System;

namespace GB.NetApi.Application.Services.UnitTests.LibrariesFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="ILogger"/> implementation
    /// </summary>
    public sealed class LoggerLibraryFixture : BaseLibraryFixture<ILogger>
    {
        protected override Mock<ILogger> InitializeDummyMock(Mock<ILogger> mock)
        {
            mock.Setup(m => m.Log(It.IsAny<ELogLevel>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            mock.Setup(m => m.Log(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            return mock;
        }
    }
}
