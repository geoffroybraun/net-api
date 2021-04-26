using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using GB.NetApi.Application.WebApi.Models;
using GB.NetApi.Domain.Models.Interfaces.Services;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Validators
{
    /// <summary>
    /// Validate a <see cref="LoginRequest"/> when entering the API
    /// </summary>
    public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        #region Fields

        private readonly ITranslator Translator;
        private readonly UserManager<UserDao> Manager;

        #endregion

        public LoginRequestValidator(ITranslator translator, UserManager<UserDao> manager)
        {
            Translator = translator ?? throw new ArgumentNullException(nameof(translator));
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));

            RuleFor((e) => e.Email)
                .Must((e) => !string.IsNullOrEmpty(e) && !string.IsNullOrWhiteSpace(e))
                .WithMessage((e) => translator.GetString("StringIsNullOrEmptyOrWhiteSpace", new[] { nameof(LoginRequest.Email) }));
            RuleFor(e => e.Email)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage((e) => translator.GetString("InvalidEmail", new[] { e.Email }));
            RuleFor(e => e.Password)
                .Must((e) => !string.IsNullOrEmpty(e) && !string.IsNullOrWhiteSpace(e))
                .WithMessage(translator.GetString("StringIsNullOrEmptyOrWhiteSpace", new[] { nameof(LoginRequest.Password) }));
        }

        public override ValidationResult Validate(ValidationContext<LoginRequest> context)
        {
            return ValidateAsync(context).Result;
        }

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<LoginRequest> context, CancellationToken cancellation = default)
        {
            var result = await base.ValidateAsync(context, cancellation).ConfigureAwait(false);

            if (result.IsValid)
                await IsValidAsync(context.InstanceToValidate, result).ConfigureAwait(false);

            return result;
        }

        #region Private methods

        private async Task IsValidAsync(LoginRequest request, ValidationResult result)
        {
            var user = await Manager.FindByEmailAsync(request.Email).ConfigureAwait(false);

            if (user is null)
            {
                var userNotFoundError = new ValidationFailure(nameof(LoginRequest), Translator.GetString("UserNotFound", request.Email));
                result.Errors.Add(userNotFoundError);

                return;
            }

            var isPasswordValid = await Manager.CheckPasswordAsync(user, request.Password).ConfigureAwait(false);

            if (!isPasswordValid)
            {
                var invalidPassword = new ValidationFailure(nameof(LoginRequest), Translator.GetString("InvalidPassword"));
                result.Errors.Add(invalidPassword);
            }
        }

        #endregion
    }
}
