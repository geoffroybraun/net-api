using GB.NetApi.Application.Services.Interfaces.Commands;
using System;

namespace GB.NetApi.Application.Services.Commands.Logs
{
    /// <summary>
    /// Represents a command to write an exception as a log
    /// </summary>
    public sealed record CreateExceptionLogCommand : ICommand<bool>
    {
        public Exception Exception { get; init; }
    }
}
