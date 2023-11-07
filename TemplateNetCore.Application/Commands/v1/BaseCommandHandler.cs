using Microsoft.Extensions.Logging;

namespace TemplateNetCore.Application.Commands.v1
{
    public abstract class BaseCommandHandler<THandler>
    {
        protected readonly ILogger<THandler> _logger;

        protected BaseCommandHandler(ILogger<THandler> logger)
        {
            _logger = logger;
        }
    }
}
