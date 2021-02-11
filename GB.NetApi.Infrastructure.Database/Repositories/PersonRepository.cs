using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Entities.Filters;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
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

        public Task<IEnumerable<Person>> FilterAsync(PersonFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> ListAsync()
        {
            throw new NotImplementedException();
        }

        async Task<bool> IPersonRepository.CreateAsync(Person person) => await CreateAsync(person).ConfigureAwait(false);
    }
}
