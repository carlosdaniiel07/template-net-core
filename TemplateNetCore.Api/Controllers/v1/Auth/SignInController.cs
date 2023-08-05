using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.Commands.v1.Auth.SignIn;

namespace TemplateNetCore.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/sign-in")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class SignInController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SignInController(IMediator mediator) =>
            _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(SignInCommandResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] SignInCommand command) =>
            Ok(await _mediator.Send(command));
    }
}
