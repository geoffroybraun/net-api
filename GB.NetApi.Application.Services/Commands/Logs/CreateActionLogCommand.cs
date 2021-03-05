using GB.NetApi.Application.Services.Interfaces.Commands;

namespace GB.NetApi.Application.Services.Commands.Logs
{
    /// <summary>
    /// Register a command to create a log for every called controller action
    /// </summary>
    public sealed record CreateActionLogCommand : ICommand<bool>
    {
        public string ActionName { get; init; }

        public string ControllerName { get; init; }

        public double ExecutionTime { get; init; }
    }
}
