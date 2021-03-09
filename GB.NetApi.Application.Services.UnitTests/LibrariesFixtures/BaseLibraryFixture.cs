using Moq;

namespace GB.NetApi.Application.Services.UnitTests.LibrariesFixtures
{
    /// <summary>
    /// Represents an abstract <see cref="TLibrary"/> dummy implementation
    /// </summary>
    /// <typeparam name="TService">The type to mock</typeparam>
    public abstract class BaseLibraryFixture<TLibrary> where TLibrary : class
    {
        #region Fields

        private readonly Mock<TLibrary> DummyMock;

        #endregion

        #region Properties

        public TLibrary Dummy => DummyMock.Object;

        #endregion

        protected BaseLibraryFixture() => DummyMock = InitializeDummyMock(new Mock<TLibrary>());

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TLibrary> InitializeDummyMock(Mock<TLibrary> mock);
    }
}
