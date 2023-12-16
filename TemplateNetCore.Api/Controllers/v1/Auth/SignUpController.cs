using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Application.Commands.v1.Auth.SignUp;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/sign-up")]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class SignUpController : BaseController
    {
        public SignUpController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [ProducesResponseType(typeof(SignUpCommandResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Login([FromBody] SignUpCommand command) =>
            GenerateResponse(await _mediator.Send(command));
    }
}
