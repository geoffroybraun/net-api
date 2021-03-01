using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.Interfaces;
using GB.NetApi.Infrastructure.Database.Repositories.Commons;
using GB.NetApi.Infrastructure.Libraries.Handlers;
using Moq;

namespace GB.NetApi.Application.Services.IntegrationTests.DataFixtures
{
    /// <summary>
    /// Represents an abstract <see cref="TRepository"/> implementation
    /// </summary>
    /// <typeparam name="TRepository">The type to mock</typeparam>
    public abstract class BaseDataFixture<TRepository> where TRepository : class
    {
        #region Fields

        private readonly Mock<TRepository> BrokenMock;
        private readonly Mock<TRepository> NullMock;

        #endregion

        #region Properties


        protected readonly ICommonRepository CommonRepository;

        protected readonly ICommonReadableRepository CommonReadableRepository;

        protected readonly ICommonWritableRepository CommonWritableRepository;

        public TRepository Broken => BrokenMock.Object;

        public TRepository Null => NullMock.Object;

        public TRepository Dummy => InitializeDummy();

        #endregion

        protected BaseDataFixture()
        {
            BrokenMock = InitializeBrokenMock(new Mock<TRepository>());
            NullMock = InitializeNullMock(new Mock<TRepository>());
            CommonRepository = new CommonRepository(() => new DummyDbContext(), new TaskHandler());
            CommonReadableRepository = new CommonReadableRepository(CommonRepository);
            CommonWritableRepository = new CommonWritableRepository(CommonRepository);
        }

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TRepository> InitializeBrokenMock(Mock<TRepository> mock);

        /// <summary>
        /// Delegate the mock initialization to the deriving class
        /// </summary>
        /// <param name="mock">The mock to set up</param>
        /// <returns>The set up mock</returns>
        protected abstract Mock<TRepository> InitializeNullMock(Mock<TRepository> mock);

        /// <summary>
        /// Delegate the initialization to the deriving class
        /// </summary>
        /// <returns>The initialized implementation</returns>
        protected abstract TRepository InitializeDummy();
    }
}
