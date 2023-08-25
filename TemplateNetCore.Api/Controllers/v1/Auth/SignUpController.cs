﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.Commands.v1.Auth.SignUp;

namespace TemplateNetCore.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/sign-up")]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class SignUpController : BaseController
    {
        public SignUpController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [ProducesResponseType(typeof(SignUpCommandResponse), 200)]
        public async Task<IActionResult> Login([FromBody] SignUpCommand command) =>
            Ok(await _mediator.Send(command));
    }
}
