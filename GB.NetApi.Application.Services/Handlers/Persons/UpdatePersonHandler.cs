using GB.NetApi.Application.Services.Collectors;
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
        private readonly ITranslator Translator;
        private readonly MessagesCollector Collector;
        private readonly PersonValidator Validator;

        #endregion

        public UpdatePersonHandler(IPersonRepository repository, ITranslator translator)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Translator = translator ?? throw new ArgumentNullException(nameof(translator));
            Collector = new MessagesCollector();
            Validator = new PersonValidator();
            Validator.SendErrorMessageEvent += Translate;
        }

        public override async Task<bool> RunAsync(UpdatePersonCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            var person = command.Transform();

            if (!await IsValidForUpdateAsync(person).ConfigureAwait(false))
                throw new EntityValidationException(Collector.Messages);

            return await Repository.UpdateAsync(person).ConfigureAwait(false);
        }

        #region Private methods

        private void Translate(string message, object[] parameters)
        {
            message = Translator.GetString(message, parameters);
            Collector.Collect(message);
        }

        private async Task<bool> IsValidForUpdateAsync(Person person)
        {
            var result = true;
            result &= Validator.IsValidWithID(person, DateTime.UtcNow);
            result &= await Repository.ExistAsync(person.ID).ConfigureAwait(false);
            result &= !await Repository.ExistAsync(person).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
