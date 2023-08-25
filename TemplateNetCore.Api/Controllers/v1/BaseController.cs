using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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
    }
}
