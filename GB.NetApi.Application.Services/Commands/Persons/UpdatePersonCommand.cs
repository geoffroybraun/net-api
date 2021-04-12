using GB.NetApi.Application.Services.Interfaces.Commands;
using GB.NetApi.Domain.Models.Entities;
using System;

namespace GB.NetApi.Application.Services.Commands.Persons
{
    /// <summary>
    /// Represents a command to update an existing <see cref="Person"/> entity
    /// </summary>
    [Serializable]
    public sealed record UpdatePersonCommand : ICommand<bool>
    {
        #region Properties

        public int ID { get; set; }

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public DateTime Birthdate { get; init; }

        #endregion

        public static implicit operator Person(UpdatePersonCommand command) => new Person()
        {
            Birthdate = command.Birthdate,
            Firstname = command.Firstname,
            ID = command.ID,
            Lastname = command.Lastname
        };
    }
}
