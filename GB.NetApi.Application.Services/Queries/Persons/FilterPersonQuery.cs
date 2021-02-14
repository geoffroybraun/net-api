using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Interfaces.Queries;
using GB.NetApi.Domain.Models.Entities.Filters;
using GB.NetApi.Domain.Models.Interfaces.Entities;
using System.Collections.Generic;

namespace GB.NetApi.Application.Services.Queries.Persons
{
    /// <summary>
    /// Represents a query to retrieve filtered <see cref="PersonDto"/>
    /// </summary>
    public sealed class FilterPersonQuery : IQuery<IEnumerable<PersonDto>>, ITransformable<PersonFilter>
    {
        #region Properties

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public int BirthYear { get; init; }

        public int BirthMonth { get; init; }

        public int BirthDay { get; init; }

        #endregion

        public PersonFilter Transform() => new PersonFilter()
        {
            BirthDay = BirthDay,
            BirthMonth = BirthMonth,
            BirthYear = BirthYear,
            Firstname = Firstname,
            Lastname = Lastname
        };
    }
}
