using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
        where TRequest : IRequest<Result<TResponse>>
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorBehavior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateAsyncScope();

            var validator = scope.ServiceProvider.GetService<IValidator<TRequest>>();
            var hasValidator = validator != null;

            if (!hasValidator)
                return await next();

            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
                return await next();

            var firstError = validationResult.Errors[0];

            return Result<TResponse>.Failure(new Error(firstError.ErrorCode, firstError.ErrorMessage));
        }
    }
}
