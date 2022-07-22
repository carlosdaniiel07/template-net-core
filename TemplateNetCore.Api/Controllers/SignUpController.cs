using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.Commands.SignUp;

namespace TemplateNetCore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/sign-up")]
    public class SignUpController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SignUpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<SignUpCommandResponse>> SignUp([FromBody] SignUpCommand command) =>
            Ok(await _mediator.Send(command));
    }
}
