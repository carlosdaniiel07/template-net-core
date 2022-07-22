using FluentValidation;
using FluentValidation.Results;

namespace TemplateNetCore.Domain.Models
{
    public class Validator<TCommand, TValidator> where TValidator : AbstractValidator<TCommand>, new()
    {
        private readonly TValidator _validator;
        private readonly TCommand _command;
        private ValidationResult validationResult;

        public Validator(TCommand command)
        {
            _command = command;
            _validator = new TValidator();
        }

        public bool IsValid()
        {
            validationResult = _validator.Validate(_command);
            return validationResult.IsValid;
        }

        public string GetFirstError()
        {
            return validationResult.Errors
                .FirstOrDefault()?.ErrorMessage;
        }

        public IEnumerable<string> GetErrors()
        {
            return validationResult.Errors
                .Select(error => error.ErrorMessage)
                .ToList();
        }
    }
}