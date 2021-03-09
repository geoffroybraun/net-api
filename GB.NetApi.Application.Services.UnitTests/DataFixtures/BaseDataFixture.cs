using Moq;

namespace GB.NetApi.Application.Services.UnitTests.DataFixtures
{
    /// <summary>
    /// Represents an abstract <see cref="TRepository"/> dummy implementation
    /// </summary>
    /// <typeparam name="TRepository">The type to mock</typeparam>
    public abstract class BaseDataFixture<TRepository> where TRepository : class
    {
        #region Fields

        private readonly Mock<TRepository> DummyMock;

        #endregion

        #region Properties

        public TRepository Dummy => DummyMock.Object;

        #endregion

        protected BaseDataFixture()
        {
            DummyMock = InitializeDummyMock(new Mock<TRepository>());
        }

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TRepository> InitializeDummyMock(Mock<TRepository> mock);
    }
}
