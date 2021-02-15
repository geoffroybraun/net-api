using GB.NetApi.Application.Services.Interfaces.Commands;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Entities;
using System;

namespace GB.NetApi.Application.Services.Commands.Persons
{
    /// <summary>
    /// Represents a command to update an existing <see cref="Person"/> entity
    /// </summary>
    [Serializable]
    public sealed record UpdatePersonCommand : ICommand<bool>, ITransformable<Person>
    {
        #region Properties

        public int ID { get; init; }

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public DateTime Birthdate { get; init; }

        #endregion

        public Person Transform() => new Person()
        {
            Birthdate = Birthdate,
            Firstname = Firstname,
            ID = ID,
            Lastname = Lastname
        };
    }
}
