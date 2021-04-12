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
    /// Run a <see cref="CreatePersonCommand"/> command
    /// </summary>
    public sealed class CreatePersonHandler : BaseCommandHandler<CreatePersonCommand, bool>
    {
        #region Fields

        private readonly IPersonRepository Repository;
        private readonly PersonValidator Validator;

        #endregion

        public CreatePersonHandler(IPersonRepository repository, ITranslator translator) : base(translator)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Validator = new PersonValidator();
            Validator.SendErrorMessageEvent += TranslateAndCollect;
        }

        public override async Task<bool> RunAsync(CreatePersonCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (!await IsValidForCreateAsync(command, DateTime.UtcNow).ConfigureAwait(false))
                throw new EntityValidationException(Collector.Messages);

            return await Repository.CreateAsync(command).ConfigureAwait(false);
        }

        #region Private methods

        private async Task<bool> IsValidForCreateAsync(Person person, DateTime maxBirthdate)
        {
            var result = true;
            result &= Validator.IsValid(person, maxBirthdate);
            result &= !await ExistAsync(person).ConfigureAwait(false);

            return result;
        }

        private async Task<bool> ExistAsync(Person person)
        {
            if (await Repository.ExistAsync(person).ConfigureAwait(false))
                return true;

            TranslateAndCollect("ExistingPerson", new[] { person.Firstname, person.Lastname, person.Birthdate.ToString("yyyy-MM-dd") });

            return false;
        }

        #endregion
    }
}
