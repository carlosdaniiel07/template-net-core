using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.Commands.CreateTransaction;

namespace TemplateNetCore.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateTransactionCommandResponse>> Post([FromBody] CreateTransactionCommand command) =>
            Ok(await _mediator.Send(command));
    }
}
