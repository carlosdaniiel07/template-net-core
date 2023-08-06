using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INotificationContextService _notificationContextService;

        public ValidatorBehavior(IServiceProvider serviceProvider, INotificationContextService notificationContextService)
        {
            _serviceProvider = serviceProvider;
            _notificationContextService = notificationContextService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateAsyncScope();

            var validator = scope.ServiceProvider.GetService<IValidator<TRequest>>();
            var hasValidator = validator != null;

            if (!hasValidator)
                return await next();

            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
                return await next();

            foreach (var error in validationResult.Errors)
                _notificationContextService.AddNotification(error.ErrorMessage);
            
            return default;
        }
    }
}
