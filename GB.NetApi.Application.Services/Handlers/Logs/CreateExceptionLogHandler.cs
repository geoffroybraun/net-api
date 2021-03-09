using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers.Logs
{
    /// <summary>
    /// Run a <see cref="CreateExceptionLogCommand"/> command
    /// </summary>
    public sealed class CreateExceptionLogHandler : BaseCommandHandler<CreateExceptionLogCommand, bool>
    {
        #region Fields

        private readonly ILogger Logger;

        #endregion

        public CreateExceptionLogHandler(ILogger logger) => Logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public override Task<bool> RunAsync(CreateExceptionLogCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (command.Exception is null)
                return Task.FromResult(false);

            Logger.Log(command.Exception);

            return Task.FromResult(true);
        }
    }
}
