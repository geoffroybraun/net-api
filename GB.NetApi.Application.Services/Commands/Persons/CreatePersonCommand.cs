using GB.NetApi.Application.Services.Interfaces.Commands;
using GB.NetApi.Domain.Models.Entities;
using System;

namespace GB.NetApi.Application.Services.Commands.Persons
{
    /// <summary>
    /// Represents a command to create a new <see cref="Person"/> entity
    /// </summary>
    [Serializable]
    public sealed record CreatePersonCommand : ICommand<bool>
    {
        #region Properties

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public DateTime Birthdate { get; init; } = DateTime.MaxValue;

        #endregion

        public static implicit operator Person(CreatePersonCommand command) => new Person()
        {
            Birthdate = command.Birthdate,
            Firstname = command.Firstname,
            Lastname = command.Lastname
        };
    }
}
