using FluentValidation;
using FluentValidation.Validators;
using GB.NetApi.Application.WebApi.Models;
using GB.NetApi.Domain.Models.Interfaces.Services;
using System;

namespace GB.NetApi.Application.WebApi.Validators
{
    /// <summary>
    /// Validate a <see cref="LoginRequest"/> when entering the API
    /// </summary>
    public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator(ITranslator translator)
        {
            if (translator is null)
                throw new ArgumentNullException(nameof(translator));

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
    }
}
