using GB.NetApi.Domain.Models.Interfaces.Services;
using Moq;

namespace GB.NetApi.Application.Services.UnitTests.ServicesFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="ITranslator"/> implementation
    /// </summary>
    public sealed class TranslatorServiceFixture : BaseServiceFixture<ITranslator>
    {
        protected override Mock<ITranslator> InitializeDummyMock(Mock<ITranslator> mock)
        {
            mock.Setup(m => m.GetString(It.IsAny<string>())).Returns("Dummy");
            mock.Setup(m => m.GetString(It.IsAny<string>(), It.IsAny<object[]>())).Returns("Dummy");

            return mock;
        }
    }
}
