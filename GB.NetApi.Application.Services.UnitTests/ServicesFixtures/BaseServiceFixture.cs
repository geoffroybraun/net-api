using Moq;

namespace GB.NetApi.Application.Services.UnitTests.ServicesFixtures
{
    /// <summary>
    /// Represents an abstract <see cref="TService"/> dummy implementation
    /// </summary>
    /// <typeparam name="TService">The type to mock</typeparam>
    public abstract class BaseServiceFixture<TService> where TService : class
    {
        #region Fields

        private readonly Mock<TService> DummyMock;

        #endregion

        #region Properties

        public TService Dummy => DummyMock.Object;

        #endregion

        protected BaseServiceFixture()
        {
            DummyMock = InitializeDummyMock(new Mock<TService>());
        }

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TService> InitializeDummyMock(Mock<TService> mock);
    }
}
