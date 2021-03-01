using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Models.Interfaces.Services;
using GB.NetApi.Domain.Services.Extensions;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers.Persons
{
    /// <summary>
    /// Run a <see cref="DeletePersonCommand"/> command
    /// </summary>
    public sealed class DeletePersonHandler : BaseCommandHandler<DeletePersonCommand, bool>
    {
        #region Fields

        private readonly IPersonRepository Repository;

        #endregion

        public DeletePersonHandler(IPersonRepository repository, ITranslator translator) : base(translator)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<bool> RunAsync(DeletePersonCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (!await IsValidForDeleteAsync(command).ConfigureAwait(false))
                throw new EntityValidationException(Collector.Messages);

            return await Repository.DeleteAsync(command.ID).ConfigureAwait(false);
        }

        #region Private methods

        private async Task<bool> IsValidForDeleteAsync(DeletePersonCommand command)
        {
            var result = true;
            result &= IsIDValid(command.ID);
            result &= await ExistAsync(command.ID).ConfigureAwait(false);

            return result;
        }

        private bool IsIDValid(int id)
        {
            if (id.IsSuperiorTo(0))
                return true;

            TranslateAndCollect("IntegerMustBeSuperiorTo", new object[] { "ID", id });

            return false;
        }

        private async Task<bool> ExistAsync(int ID)
        {
            if (await Repository.ExistAsync(ID).ConfigureAwait(false))
                return true;

            TranslateAndCollect("InexistingID", new[] { ID });

            return false;
        }

        #endregion
    }
}
