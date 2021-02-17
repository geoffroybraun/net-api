using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
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

        public CreatePersonHandler(IPersonRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Validator = new PersonValidator();
        }

        public override async Task<bool> RunAsync(CreatePersonCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            var person = command.Transform();
            var canBeCreated = await IsValidForCreateAsync(person, DateTime.Now).ConfigureAwait(false);

            if (!canBeCreated)
                throw new EntityValidationException();

            return await Repository.CreateAsync(person).ConfigureAwait(false);
        }

        #region Private methods

        private async Task<bool> IsValidForCreateAsync(Person person, DateTime maxBirthdate)
        {
            var result = true;
            result &= Validator.IsValid(person, maxBirthdate);
            result &= !await Repository.ExistAsync(person).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
