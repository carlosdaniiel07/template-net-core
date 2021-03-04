using Microsoft.AspNetCore.Mvc;

using TemplateNetCore.Domain.Dto.Transactions;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Interfaces.Transactions;

namespace TemplateNetCore.Api.Controllers.Transactions
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public ActionResult<Transaction> Index([FromBody] PostTransactionDto postTransactionDto)
        {
            return Ok(_transactionService.Save(postTransactionDto));
        }
    }
}

