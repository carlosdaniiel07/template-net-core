using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Application.Commands.v1.Auth.SignIn;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/sign-in")]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class SignInController(IMediator mediator, INotificationContext notificationContext) : BaseController(mediator, notificationContext)
    {
        [HttpPost]
        [ProducesResponseType(typeof(SignInCommandResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Login([FromBody] SignInCommand command) =>
            GenerateResponse(await _mediator.Send(command));
    }
}
