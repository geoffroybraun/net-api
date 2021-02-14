using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Interfaces.Queries;

namespace GB.NetApi.Application.Services.Queries.Persons
{
    /// <summary>
    /// Represents a query to retrieve a <see cref="PersonDto"/>
    /// </summary>
    public sealed record GetSinglePersonQuery : IQuery<PersonDto>
    {
        public int ID { get; init; }
    }
}
