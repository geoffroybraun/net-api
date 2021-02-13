using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures
{
    /// <summary>
    /// Represents a dummy <see cref="BaseDbContext"/> implementation
    /// </summary>
    public sealed class BaseDbContextDataFixture : BaseDataFixture<BaseDbContext>
    {
        protected override Mock<BaseDbContext> InitializeBrokenMock(Mock<BaseDbContext> mock)
        {
            mock.Setup(m => m.Persons)
                .Throws<NotImplementedException>();

            return mock;
        }

        protected override BaseDbContext InitializeDummy() => new DummyDbContext();

        protected override Mock<BaseDbContext> InitializeNullMock(Mock<BaseDbContext> mock)
        {
            mock.Setup(m => m.Persons)
                .Returns(default(DbSet<PersonDao>));

            return mock;
        }
    }
}
