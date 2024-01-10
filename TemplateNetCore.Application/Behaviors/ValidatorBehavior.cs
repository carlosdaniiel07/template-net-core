using FluentValidation;
using MediatR;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly INotificationContext _notificationContext;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, INotificationContext notificationContext)
        {
            _validators = validators;
            _notificationContext = notificationContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validator = _validators.FirstOrDefault();

            if (validator is null)
                return await next();

            var validationResult = validator.Validate(new ValidationContext<TRequest>(request));

            if (validationResult.IsValid)
                return await next();

            var firstError = validationResult.Errors[0];

            _notificationContext.AddError(new Error(firstError.ErrorCode, firstError.ErrorMessage));

            return default;
        }
    }
}
