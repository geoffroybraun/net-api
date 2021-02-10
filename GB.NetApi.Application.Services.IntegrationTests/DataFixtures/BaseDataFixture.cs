using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Libraries.Handlers;
using Moq;
using System;

namespace GB.NetApi.Application.Services.IntegrationTests.DataFixtures
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

        protected readonly Func<BaseDbContext> ContextFunction;

        protected readonly ITaskHandler TaskHandler;

        public T Broken => BrokenMock.Object;

        public T Null => NullMock.Object;

        public T Dummy => InitializeDummy();

        #endregion

        protected BaseDataFixture()
        {
            BrokenMock = InitializeBrokenMock(new Mock<T>());
            NullMock = InitializeNullMock(new Mock<T>());
            ContextFunction = () => new DummyDbContext();
            TaskHandler = new TaskHandler();
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
