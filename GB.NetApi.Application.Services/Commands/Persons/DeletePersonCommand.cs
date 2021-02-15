using GB.NetApi.Application.Services.Interfaces.Commands;
using System;

namespace GB.NetApi.Application.Services.Commands.Persons
{
    /// <summary>
    /// Represents a command to delete <see cref="Domain.Models.Entities.Person"/> entity
    /// </summary>
    [Serializable]
    public sealed record DeletePersonCommand : ICommand<bool>
    {
        public int ID { get; init; }
    }
}
