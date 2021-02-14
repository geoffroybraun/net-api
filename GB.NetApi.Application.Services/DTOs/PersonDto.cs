using GB.NetApi.Domain.Models.Entities;
using System;

namespace GB.NetApi.Application.Services.DTOs
{
    /// <summary>
    /// Represents a DTO which can be instanciated using a <see cref="Person"/> entity
    /// </summary>
    [Serializable]
    public sealed record PersonDto
    {
        #region Properties

        public int ID { get; init; }

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public DateTime Birthdate { get; init; }

        #endregion

        public static explicit operator PersonDto(Person entity)
        {
            if (entity is null)
                return default;

            return new PersonDto()
            {
                Birthdate = entity.Birthdate,
                Firstname = entity.Firstname,
                ID = entity.ID,
                Lastname = entity.Lastname
            };
        }
    }
}
