using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.Persons;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Entities.Filters;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers.Persons
{
    /// <summary>
    /// Executes a <see cref="FilterPersonQuery"/> query
    /// </summary>
    public sealed class FilterPersonHandler : QueryHandler<FilterPersonQuery, IEnumerable<PersonDto>>
    {
        #region Fields

        private readonly IPersonRepository Repository;

        #endregion

        public FilterPersonHandler(IPersonRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public override async Task<IEnumerable<PersonDto>> ExecuteAsync(FilterPersonQuery query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));

            var result = await FilterAsync(query.Transform())
                .ConfigureAwait(false);

            return Transform(result);
        }

        #region Private methods

        private async Task<IEnumerable<Person>> FilterAsync(PersonFilter filter)
        {
            if (PersonFilterValidator.IsNotValid(filter))
                return await Repository.ListAsync()
                    .ConfigureAwait(false);

            return await Repository.FilterAsync(filter)
                .ConfigureAwait(false);
        }

        private static IEnumerable<PersonDto> Transform(IEnumerable<Person> persons) => persons.IsNotNullNorEmpty() ? persons.Select(Transform) : default;

        private static PersonDto Transform(Person person) => (PersonDto)person;

        #endregion
    }
}
