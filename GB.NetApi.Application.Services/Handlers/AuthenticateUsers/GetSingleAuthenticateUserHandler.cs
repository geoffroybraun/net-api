using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.AuthenticateUsers;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Services.Extensions;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers.AuthenticateUsers
{
    /// <summary>
    /// Executes an <see cref="GetSingleAuthenticateUserQuery"/> query
    /// </summary>
    public sealed class GetSingleAuthenticateUserHandler : BaseQueryHandler<GetSingleAuthenticateUserQuery, AuthenticateUserDto>
    {
        #region Fields

        private readonly IAuthenticateUserRepository Repository;

        #endregion

        public GetSingleAuthenticateUserHandler(IAuthenticateUserRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<AuthenticateUserDto> ExecuteAsync(GetSingleAuthenticateUserQuery query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));

            if (query.UserEmail.IsNullOrEmptyOrWhiteSpace())
                throw new EntityValidationException();

            return (AuthenticateUserDto)await Repository.GetAsync(query.UserEmail).ConfigureAwait(false);
        }
    }
}
