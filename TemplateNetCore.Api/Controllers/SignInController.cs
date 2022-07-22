using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.Commands.SignIn;

namespace TemplateNetCore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/sign-in")]
    public class SignInController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SignInController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<SignInCommandResponse>> SignIn([FromBody] SignInCommand command) =>
            Ok(await _mediator.Send(command));
    }
}