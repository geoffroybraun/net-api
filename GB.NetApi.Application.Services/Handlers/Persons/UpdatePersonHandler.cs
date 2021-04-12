using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Models.Interfaces.Services;
using GB.NetApi.Domain.Services.Validators;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers.Persons
{
    /// <summary>
    /// Run a <see cref="UpdatePersonCommand"/> command
    /// </summary>
    public sealed class UpdatePersonHandler : BaseCommandHandler<UpdatePersonCommand, bool>
    {
        #region Fields

        private readonly IPersonRepository Repository;
        private readonly PersonValidator Validator;

        #endregion

        public UpdatePersonHandler(IPersonRepository repository, ITranslator translator) : base(translator)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Validator = new PersonValidator();
            Validator.SendErrorMessageEvent += TranslateAndCollect;
        }

        public override async Task<bool> RunAsync(UpdatePersonCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (!await IsValidForUpdateAsync(command).ConfigureAwait(false))
                throw new EntityValidationException(Collector.Messages);

            return await Repository.UpdateAsync(command).ConfigureAwait(false);
        }

        #region Private methods

        private async Task<bool> IsValidForUpdateAsync(Person person)
        {
            var result = true;
            result &= Validator.IsValidWithID(person, DateTime.UtcNow);
            result &= await ExistAsync(person.ID).ConfigureAwait(false);
            result &= !await ExistAsync(person).ConfigureAwait(false);

            return result;
        }

        private async Task<bool> ExistAsync(int ID)
        {
            if (await Repository.ExistAsync(ID).ConfigureAwait(false))
                return true;

            TranslateAndCollect("InexistingID", new[] { ID });

            return false;
        }

        private async Task<bool> ExistAsync(Person person)
        {
            if (await Repository.ExistAsync(person).ConfigureAwait(false))
            {
                TranslateAndCollect("ExistingPerson", new[] { person.Firstname, person.Lastname, person.Birthdate.ToString("yyyy-MM-dd") });

                return true;
            }

            return false;
        }

        #endregion
    }
}
