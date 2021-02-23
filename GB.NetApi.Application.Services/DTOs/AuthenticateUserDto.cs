using GB.NetApi.Domain.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GB.NetApi.Application.Services.DTOs
{
    /// <summary>
    /// Represents a DTO which can be instanciated using a <see cref="AuthenticateUser"/> entity
    /// </summary>
    [Serializable]
    public sealed record AuthenticateUserDto
    {
        #region Properties

        public string ID { get; init; }

        public string Name { get; init; }

        public IEnumerable<Claim> Claims { get; init; }

        public IEnumerable<string> PermissionNames { get; init; }

        #endregion

        public static explicit operator AuthenticateUserDto(AuthenticateUser entity)
        {
            if (entity is null)
                return default;

            return new AuthenticateUserDto()
            {
                Claims = entity.Claims,
                ID = entity.ID,
                PermissionNames = entity.PermissionNames,
                Name = entity.Name
            };
        }
    }
}
