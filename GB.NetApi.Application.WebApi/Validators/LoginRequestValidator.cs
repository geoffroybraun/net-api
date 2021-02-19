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
            RuleFor(e => e.UserName).NotNull().NotEmpty();
            RuleFor(e => e.Password).NotNull().NotEmpty();
        }
    }
}
