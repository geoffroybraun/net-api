using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Entities.Filters;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories
{
    public sealed class PersonRepository : BaseWritableRepository<PersonDao, Person>, IPersonRepository
    {
        public PersonRepository(Func<BaseDbContext> contextFunction, ITaskHandler taskHandler) : base(contextFunction, taskHandler) { }

        public async Task<bool> ExistAsync(Person person)
        {
            var model = new AnyModel<PersonDao>()
            {
                Any = (dao) => dao.Birthdate == person.Birthdate && dao.Firstname == person.Firstname && dao.Lastname == person.Lastname
            };

            return await AnyAsync(model).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Person>> FilterAsync(PersonFilter filter)
        {
            var model = GetModelFromFilter(filter);

            return await ToListAsync(model).ConfigureAwait(false);
        }

        public async Task<Person> GetAsync(int ID)
        {
            var model = new SingleModel<PersonDao>()
            {
                SingleOrDefault = (dao) => dao.ID == ID
            };

            return await SingleAsync(model).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Person>> ListAsync() => await ToListAsync().ConfigureAwait(false);

        async Task<bool> IPersonRepository.CreateAsync(Person person) => await CreateAsync(person).ConfigureAwait(false);

        #region Private methods

        private static WhereManyModel<PersonDao> GetModelFromFilter(PersonFilter filter)
        {
            var functions = new List<Expression<Func<PersonDao, bool>>>();

            if (filter.Firstname.IsNotNullNorEmptyNorWhiteSpace())
                functions.Add((dao) => dao.Firstname == filter.Firstname);

            if (filter.Lastname.IsNotNullNorEmptyNorWhiteSpace())
                functions.Add((dao) => dao.Lastname == filter.Lastname);

            if (filter.BirthYear.IsSuperiorTo(0))
                functions.Add((dao) => dao.Birthdate.Year == filter.BirthYear);

            if (filter.BirthMonth.IsSuperiorTo(0))
                functions.Add((dao) => dao.Birthdate.Month == filter.BirthMonth);

            if (filter.BirthDay.IsSuperiorTo(0))
                functions.Add((dao) => dao.Birthdate.Day == filter.BirthDay);

            return new WhereManyModel<PersonDao>() { WhereMany = functions };
        }

        #endregion
    }
}
