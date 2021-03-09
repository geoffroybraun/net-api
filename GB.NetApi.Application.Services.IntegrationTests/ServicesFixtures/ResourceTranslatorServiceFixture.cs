using GB.NetApi.Application.Services.Translators;
using GB.NetApi.Domain.Models.Interfaces.Services;
using Moq;
using System;
using System.Globalization;

namespace GB.NetApi.Application.Services.IntegrationTests.ServicesFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="ITranslator"/> implementation
    /// </summary>
    public sealed class ResourceTranslatorServiceFixture : BaseServiceFixture<ITranslator>
    {
        protected override Mock<ITranslator> InitializeBrokenMock(Mock<ITranslator> mock)
        {
            mock.Setup(m => m.GetString(It.IsAny<string>())).Throws<NotImplementedException>();
            mock.Setup(m => m.GetString(It.IsAny<string>(), It.IsAny<object[]>())).Throws<NotImplementedException>();

            return mock;
        }

        protected override ITranslator InitializeDummy() => new ResourceTranslator(new CultureInfo("en"));

        protected override Mock<ITranslator> InitializeNullMock(Mock<ITranslator> mock)
        {
            mock.Setup(m => m.GetString(It.IsAny<string>())).Returns(default(string));
            mock.Setup(m => m.GetString(It.IsAny<string>(), It.IsAny<object[]>())).Returns(default(string));

            return mock;
        }
    }
}
