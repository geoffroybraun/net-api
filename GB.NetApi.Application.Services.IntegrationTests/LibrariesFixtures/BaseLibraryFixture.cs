using Moq;

namespace GB.NetApi.Application.Services.IntegrationTests.LibrariesFixtures
{
    /// <summary>
    /// Represents an abstract <see cref="TLibrary"/> implementation
    /// </summary>
    /// <typeparam name="TLibrary">The type to mock</typeparam>
    public abstract class BaseLibraryFixture<TLibrary> where TLibrary : class
    {
        #region Fields

        private readonly Mock<TLibrary> BrokenMock;
        private readonly Mock<TLibrary> NullMock;

        #endregion

        #region Properties

        public TLibrary Broken => BrokenMock.Object;

        public TLibrary Null => NullMock.Object;

        public TLibrary Dummy => InitializeDummy();

        #endregion

        protected BaseLibraryFixture()
        {
            BrokenMock = InitializeBrokenMock(new Mock<TLibrary>());
            NullMock = InitializeNullMock(new Mock<TLibrary>());
        }

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TLibrary> InitializeBrokenMock(Mock<TLibrary> mock);

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TLibrary> InitializeNullMock(Mock<TLibrary> mock);

        /// <summary>
        /// Delegate the initialization to the deriving class
        /// </summary>
        /// <returns>The initialized implementation</returns>
        protected abstract TLibrary InitializeDummy();
    }
}
