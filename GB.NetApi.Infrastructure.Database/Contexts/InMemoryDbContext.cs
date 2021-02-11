using GB.NetApi.Infrastructure.Database.DAOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GB.NetApi.Infrastructure.Database.Contexts
{
    /// <summary>
    /// Represents a <see cref="BaseDbContext"/> implemenation which uses an 'in-memory' database
    /// </summary>
    public sealed class InMemoryDbContext : BaseDbContext
    {
        public InMemoryDbContext() : base(BuildOptions()) => Initialize();

        #region Private methods

        private static DbContextOptions BuildOptions()
        {
            var builder = new DbContextOptionsBuilder<InMemoryDbContext>();
            builder.UseInMemoryDatabase("NET_API");

            return builder.Options;
        }

        private void Initialize()
        {
            Persons.AddRange(GetPersons());
            SaveChanges();
        }

        private static IEnumerable<PersonDao> GetPersons()
        {
            var birthdateYear = DateTime.Now
                .AddYears(-10)
                .Year;

            return new[]
            {
                new PersonDao() { ID = 1, Firstname = "Stan", Lastname = "Marsh", Birthdate = new DateTime(birthdateYear, 10, 19) },
                new PersonDao() { ID = 2, Firstname = "Kyle", Lastname = "Broflovski", Birthdate = new DateTime(birthdateYear, 5, 26) },
                new PersonDao() { ID = 3, Firstname = "Kenny", Lastname = "MacCormick", Birthdate = new DateTime(birthdateYear, 3, 22) },
                new PersonDao() { ID = 4, Firstname = "Eric", Lastname = "Cartman", Birthdate = new DateTime(birthdateYear, 6, 1) },
            };
        }

        #endregion
    }
}
