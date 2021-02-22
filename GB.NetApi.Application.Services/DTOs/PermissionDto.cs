using GB.NetApi.Domain.Models.Entities.Identity;
using System;

namespace GB.NetApi.Application.Services.DTOs
{
    /// <summary>
    /// Represents a DTO which can be instanciated using a <see cref="Permission"/> entity
    /// </summary>
    [Serializable]
    public sealed record PermissionDto
    {
        #region Properties

        public int ID { get; init; }

        public string Name { get; init; }

        #endregion

        public static explicit operator PermissionDto(Permission entity)
        {
            if (entity is null)
                return default;

            return new PermissionDto() { ID = entity.ID, Name = entity.Name };
        }
    }
}
