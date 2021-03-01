using Moq;

namespace GB.NetApi.Application.Services.IntegrationTests.ServicesFixtures
{
    /// <summary>
    /// Represents an abstract <see cref="TService"/> implementation
    /// </summary>
    /// <typeparam name="TService">The type to mock</typeparam>
    public abstract class BaseServiceFixture<TService> where TService : class
    {
        #region Fields

        private readonly Mock<TService> BrokenMock;
        private readonly Mock<TService> NullMock;

        #endregion

        #region Properties

        public TService Broken => BrokenMock.Object;

        public TService Null => NullMock.Object;

        public TService Dummy => InitializeDummy();

        #endregion

        protected BaseServiceFixture()
        {
            BrokenMock = InitializeBrokenMock(new Mock<TService>());
            NullMock = InitializeNullMock(new Mock<TService>());
        }

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TService> InitializeBrokenMock(Mock<TService> mock);

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TService> InitializeNullMock(Mock<TService> mock);

        /// <summary>
        /// Delegate the initialization to the deriving class
        /// </summary>
        /// <returns>The initialized implementation</returns>
        protected abstract TService InitializeDummy();
    }
}
