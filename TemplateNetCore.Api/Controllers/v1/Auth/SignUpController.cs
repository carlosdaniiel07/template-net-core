using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.Commands.v1.Auth.SignUp;

namespace TemplateNetCore.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/sign-up")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class SignUpController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SignUpController(IMediator mediator) =>
            _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(SignUpCommandResponse), 200)]
        public async Task<IActionResult> Login([FromBody] SignUpCommand command) =>
            Ok(await _mediator.Send(command));
    }
}
