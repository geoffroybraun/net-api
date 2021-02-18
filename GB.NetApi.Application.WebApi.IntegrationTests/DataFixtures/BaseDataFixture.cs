using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.Interfaces;
using GB.NetApi.Infrastructure.Database.Repositories.Commons;
using GB.NetApi.Infrastructure.Libraries.Handlers;
using Moq;

namespace GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures
{
    /// <summary>
    /// Represents an abstract <see cref="T"/> implementation
    /// </summary>
    /// <typeparam name="T">The type to mock</typeparam>
    public abstract class BaseDataFixture<T> where T : class
    {
        #region Fields

        private readonly Mock<T> BrokenMock;
        private readonly Mock<T> NullMock;

        #endregion

        #region Properties

        protected readonly ICommonRepository CommonRepository;

        protected readonly ICommonReadableRepository CommonReadableRepository;

        protected readonly ICommonWritableRepository CommonWritableRepository;

        public T Broken => BrokenMock.Object;

        public T Null => NullMock.Object;

        public T Dummy => InitializeDummy();

        #endregion

        protected BaseDataFixture()
        {
            BrokenMock = InitializeBrokenMock(new Mock<T>());
            NullMock = InitializeNullMock(new Mock<T>());
            CommonRepository = new CommonRepository(() => new DummyDbContext(), new TaskHandler());
            CommonReadableRepository = new CommonReadableRepository(CommonRepository);
            CommonWritableRepository = new CommonWritableRepository(CommonReadableRepository);
        }

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<T> InitializeBrokenMock(Mock<T> mock);

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<T> InitializeNullMock(Mock<T> mock);

        /// <summary>
        /// Delegate the initialization to the deriving class
        /// </summary>
        /// <returns>The initialized implementation</returns>
        protected abstract T InitializeDummy();
    }
}
