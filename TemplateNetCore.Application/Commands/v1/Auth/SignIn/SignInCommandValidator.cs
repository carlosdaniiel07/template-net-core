using FluentValidation;
using TemplateNetCore.Domain.Commands.v1.Auth.SignIn;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
