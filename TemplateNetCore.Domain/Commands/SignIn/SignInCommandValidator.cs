using FluentValidation;

namespace TemplateNetCore.Domain.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(120);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6, 32);
        }
    }
}