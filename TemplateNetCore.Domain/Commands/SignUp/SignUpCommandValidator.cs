using FluentValidation;

namespace TemplateNetCore.Domain.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 80);

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