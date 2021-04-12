using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Interfaces.Queries;
using GB.NetApi.Domain.Models.Entities.Filters;
using System;
using System.Collections.Generic;

namespace GB.NetApi.Application.Services.Queries.Persons
{
    /// <summary>
    /// Represents a query to retrieve filtered <see cref="PersonDto"/>
    /// </summary>
    [Serializable]
    public sealed class FilterPersonQuery : IQuery<IEnumerable<PersonDto>>
    {
        #region Properties

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public int BirthYear { get; init; }

        public int BirthMonth { get; init; }

        public int BirthDay { get; init; }

        #endregion

        public static implicit operator PersonFilter(FilterPersonQuery query) => new PersonFilter()
        {
            BirthDay = query.BirthDay,
            BirthMonth = query.BirthMonth,
            BirthYear = query.BirthYear,
            Firstname = query.Firstname,
            Lastname = query.Lastname
        };
    }
}
