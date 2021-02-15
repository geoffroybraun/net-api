using GB.NetApi.Infrastructure.Database.DAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace GB.NetApi.Infrastructure.Database.Contexts
{
    /// <summary>
    /// Represents a dummy <see cref="BaseDbContext"/> implementation to use within integration tests
    /// </summary>
    public sealed class DummyDbContext : BaseDbContext
    {
        public DummyDbContext() : base(BuildOptions()) => Initialize();

        #region Private methods

        private static DbContextOptions BuildOptions()
        {
            var services = new ServiceCollection();
            var provider = services.AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DummyDbContext>();
            builder.UseInMemoryDatabase("DUMMY")
                .UseInternalServiceProvider(provider);

            return builder.Options;
        }

        private void Initialize()
        {
            Persons.AddRange(GetPersons());
            SaveChanges();
        }

        private static IEnumerable<PersonDao> GetPersons() => new[]
        {
            new PersonDao() { ID = 1, Birthdate = new DateTime(1990, 1, 1), Firstname = "Firstname", Lastname = "Lastname" },
        };

        #endregion
    }
}
