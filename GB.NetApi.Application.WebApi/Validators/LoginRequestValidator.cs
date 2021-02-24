using FluentValidation;
using GB.NetApi.Application.WebApi.Models;

namespace GB.NetApi.Application.WebApi.Validators
{
    /// <summary>
    /// Validate a <see cref="LoginRequest"/> when entering the API
    /// </summary>
    public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(e => e.Name).NotNull().NotEmpty();
            RuleFor(e => e.Email).NotNull().NotEmpty();
            RuleFor(e => e.Password).NotNull().NotEmpty();
        }
    }
}
