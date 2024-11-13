using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Api.Controllers.v1;

[ApiController]
[EnableRateLimiting("fixed")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;
    private readonly INotificationContext _notificationContext;

    protected BaseController(IMediator mediator, INotificationContext notificationContext)
    {
        _mediator = mediator;
        _notificationContext = notificationContext;
    }

    protected IActionResult GenerateResponse<TResponse>(Result<TResponse> result)
    {
        var errors = _notificationContext.Errors;

        if (!errors.IsEmpty)
        {
            var errorResult = Result<TResponse>.Failure(errors.First());
            return BadRequest(errorResult.Error);
        }

        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }
}
