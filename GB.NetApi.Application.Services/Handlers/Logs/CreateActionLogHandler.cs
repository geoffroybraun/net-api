using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Domain.Models.Enums;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Domain.Models.Interfaces.Services;
using GB.NetApi.Domain.Services.Extensions;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers.Logs
{
    /// <summary>
    /// Run a <see cref="CreateActionLogCommand"/> command
    /// </summary>
    public sealed class CreateActionLogHandler : BaseCommandHandler<CreateActionLogCommand, bool>
    {
        #region Fields

        private readonly ILogger Logger;

        #endregion

        public CreateActionLogHandler(ILogger logger, ITranslator translator) : base(translator)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override Task<bool> RunAsync(CreateActionLogCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (IsNotValid(command))
                return Task.FromResult(false);

            var message = Translator.GetString("ActionLogMessage", command.ControllerName, command.ActionName, command.ExecutionTime);
            Logger.Log(ELogLevel.Information, message);

            return Task.FromResult(true);
        }

        #region Private methods

        private static bool IsNotValid(CreateActionLogCommand command) => !IsValid(command);

        private static bool IsValid(CreateActionLogCommand command)
        {
            var result = true;
            result &= command.ActionName.IsNotNullNorEmptyNorWhiteSpace();
            result &= command.ControllerName.IsNotNullNorEmptyNorWhiteSpace();
            result &= command.ExecutionTime.IsSuperiorTo(0);

            return result;
        }

        #endregion
    }
}
