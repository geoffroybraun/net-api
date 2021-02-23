using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
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

        public DeletePersonHandler(IPersonRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public override async Task<bool> RunAsync(DeletePersonCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            var canBeDeleted = await IsValidForDeleteAsync(command).ConfigureAwait(false);

            if (!canBeDeleted)
                throw new EntityValidationException();

            return await Repository.DeleteAsync(command.ID).ConfigureAwait(false);
        }

        #region Private methods

        private async Task<bool> IsValidForDeleteAsync(DeletePersonCommand command)
        {
            var result = true;
            result &= command.ID.IsSuperiorTo(0);
            result &= await Repository.ExistAsync(command.ID).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
