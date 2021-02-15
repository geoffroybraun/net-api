using GB.NetApi.Application.Services.Interfaces.Commands;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Entities;
using System;

namespace GB.NetApi.Application.Services.Commands.Persons
{
    /// <summary>
    /// Represents a command to create a new <see cref="Person"/> entity
    /// </summary>
    [Serializable]
    public sealed record CreatePersonCommand : ICommand<bool>, ITransformable<Person>
    {
        #region Properties

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public DateTime Birthdate { get; init; }

        #endregion

        public Person Transform() => new Person()
        {
            Birthdate = Birthdate,
            Firstname = Firstname,
            Lastname = Lastname
        };
    }
}
