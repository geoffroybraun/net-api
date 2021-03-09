using GB.NetApi.Application.Services.Interfaces.Commands;
using System.Collections.Generic;

namespace GB.NetApi.Application.Services.Commands.Logs
{
    /// <summary>
    /// Represents a command to write a bad request as a log
    /// </summary>
    public sealed record CreateBadRequestLogCommand : ICommand<bool>
    {
        public string ActionName { get; init; }

        public string ControllerName { get; init; }

        public IEnumerable<string> Errors { get; init; }
    }
}
