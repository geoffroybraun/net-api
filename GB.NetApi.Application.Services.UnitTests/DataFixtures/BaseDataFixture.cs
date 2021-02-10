using Moq;

namespace GB.NetApi.Application.Services.UnitTests.DataFixtures
{
    /// <summary>
    /// Represents an abstract <see cref="T"/> dummy implementation
    /// </summary>
    /// <typeparam name="T">The type to mock</typeparam>
    public abstract class BaseDataFixture<T> where T : class
    {
        #region Fields

        private readonly Mock<T> DummyMock;

        #endregion

        #region Properties

        public T Dummy => DummyMock.Object;

        #endregion

        protected BaseDataFixture()
        {
            DummyMock = InitializeDummyMock(new Mock<T>());
        }

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<T> InitializeDummyMock(Mock<T> mock);
    }
}
