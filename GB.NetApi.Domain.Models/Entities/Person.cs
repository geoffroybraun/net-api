using System;

namespace GB.NetApi.Domain.Models.Entities
{
    /// <summary>
    /// Represents a person with firstname, lastname, and birthdate
    /// </summary>
    public sealed record Person : BaseStorableEntity
    {
        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public DateTime Birthdate { get; init; } = DateTime.MaxValue;
    }
}
