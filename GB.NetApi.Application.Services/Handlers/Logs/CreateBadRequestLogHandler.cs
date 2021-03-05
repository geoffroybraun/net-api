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
    /// Run a <see cref="CreateBadRequestLogCommand"/> command
    /// </summary>
    public sealed class CreateBadRequestLogHandler : BaseCommandHandler<CreateBadRequestLogCommand, bool>
    {
        #region Fields

        private readonly ILogger Logger;

        public CreateBadRequestLogHandler(ILogger logger, ITranslator translator) : base(translator)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        public override Task<bool> RunAsync(CreateBadRequestLogCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (IsNotValid(command))
                return Task.FromResult(false);

            var message = Translator.GetString("BadRequestLogMessage", command.ActionName, command.ControllerName, string.Join(" | ", command.Errors));
            Logger.Log(ELogLevel.Warning, message);

            return Task.FromResult(true);
        }

        #region Private methods

        private static bool IsNotValid(CreateBadRequestLogCommand command) => !IsValid(command);

        private static bool IsValid(CreateBadRequestLogCommand command)
        {
            var result = true;
            result &= command.ActionName.IsNotNullNorEmptyNorWhiteSpace();
            result &= command.ControllerName.IsNotNullNorEmptyNorWhiteSpace();
            result &= command.Errors.IsNotNullNorEmpty();

            return result;
        }

        #endregion
    }
}
