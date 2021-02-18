using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Entities.Filters;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Interfaces;
using GB.NetApi.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories
{
    public sealed class PersonRepository : IPersonRepository
    {
        #region Fields

        private readonly ICommonWritableRepository Repository;

        #endregion

        public PersonRepository(ICommonWritableRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<bool> ExistAsync(Person person)
        {
            var model = new AnyModel<PersonDao>()
            {
                Any = (dao) => dao.Birthdate == person.Birthdate && dao.Firstname == person.Firstname && dao.Lastname == person.Lastname
            };

            return await Repository.AnyAsync<PersonDao, Person>(model).ConfigureAwait(false);
        }

        public async Task<bool> ExistAsync(int ID)
        {
            var model = new AnyModel<PersonDao>()
            {
                Any = (dao) => dao.ID == ID
            };

            return await Repository.AnyAsync<PersonDao, Person>(model).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Person>> FilterAsync(PersonFilter filter)
        {
            var model = GetModelFromFilter(filter);

            return await Repository.ToListAsync<PersonDao, Person>(model).ConfigureAwait(false);
        }

        public async Task<Person> GetAsync(int ID)
        {
            var model = new SingleModel<PersonDao>()
            {
                SingleOrDefault = (dao) => dao.ID == ID
            };

            return await Repository.SingleAsync<PersonDao, Person>(model).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Person>> ListAsync() => await Repository.ToListAsync<PersonDao, Person>().ConfigureAwait(false);

        public async Task<bool> CreateAsync(Person person) => await Repository.CreateAsync<Person, PersonDao>(person).ConfigureAwait(false);

        public async Task<bool> UpdateAsync(Person person) => await Repository.UpdateAsync<Person, PersonDao>(person).ConfigureAwait(false);

        public async Task<bool> DeleteAsync(int ID) => await Repository.DeleteAsync<Person, PersonDao>(ID).ConfigureAwait(false);

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
