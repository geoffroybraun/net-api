using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.Persons;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Services.Extensions;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers.Persons
{
    /// <summary>
    /// Executes a <see cref="GetSinglePersonQuery"/> query
    /// </summary>
    public sealed class GetSinglePersonHandler : QueryHandler<GetSinglePersonQuery, PersonDto>
    {
        #region Fields

        private readonly IPersonRepository Repository;

        #endregion

        public GetSinglePersonHandler(IPersonRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public override async Task<PersonDto> ExecuteAsync(GetSinglePersonQuery query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));

            if (query.ID.IsInferiorOrEqualTo(0))
                throw new ArgumentOutOfRangeException(query.ID.ToString());

            var result = await Repository.GetAsync(query.ID).ConfigureAwait(false);

            return Transform(result);
        }

        #region Private methods

        private static PersonDto Transform(Person person) => (PersonDto)person;

        #endregion
    }
}
