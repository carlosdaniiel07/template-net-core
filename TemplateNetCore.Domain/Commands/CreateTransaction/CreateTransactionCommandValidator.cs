using FluentValidation;

namespace TemplateNetCore.Domain.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(50);
            
            RuleFor(x => x.Value)
                .InclusiveBetween(0.01m, decimal.MaxValue);
        }
    }
}