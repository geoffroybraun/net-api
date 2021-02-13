using GB.NetApi.Infrastructure.Database.Contexts;
using Moq;
using System;

namespace GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="Func{BaseDbContext}"/> implementation
    /// </summary>
    public sealed class FuncBaseDbContextDataFixture : BaseDataFixture<Func<BaseDbContext>>
    {
        #region Fields

        private static readonly BaseDbContextDataFixture Fixture = new BaseDbContextDataFixture();

        #endregion

        protected override Mock<Func<BaseDbContext>> InitializeBrokenMock(Mock<Func<BaseDbContext>> mock)
        {
            mock.Setup(m => m.Invoke())
                .Returns(Fixture.Broken);

            return mock;
        }

        protected override Func<BaseDbContext> InitializeDummy() => () => Fixture.Dummy;

        protected override Mock<Func<BaseDbContext>> InitializeNullMock(Mock<Func<BaseDbContext>> mock)
        {
            mock.Setup(m => m.Invoke())
                .Returns(Fixture.Null);

            return mock;
        }
    }
}
