using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Interfaces.Queries;
using System;

namespace GB.NetApi.Application.Services.Queries.AuthenticateUsers
{
    /// <summary>
    /// Represents a query to retrieve a <see cref="AuthenticateUserDto"/>
    /// </summary>
    [Serializable]
    public sealed record GetSingleAuthenticateUserQuery : IQuery<AuthenticateUserDto>
    {
        public string UserEmail { get; init; }
    }
}
