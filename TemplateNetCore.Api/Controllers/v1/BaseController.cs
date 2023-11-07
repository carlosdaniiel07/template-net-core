using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Api.Controllers.v1
{

    [ApiController]
    [EnableRateLimiting("fixed")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected IActionResult GenerateResponse<TCommandResponse>(Result<TCommandResponse> result) =>
            result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }
}
