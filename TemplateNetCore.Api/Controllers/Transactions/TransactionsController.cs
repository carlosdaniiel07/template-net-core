using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TemplateNetCore.Domain.Dto.Transactions;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Interfaces.Transactions;

namespace TemplateNetCore.Api.Controllers.Transactions
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService transactionService)
        {
            _service = transactionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public ActionResult<Transaction> Save([FromBody] PostTransactionDto postTransactionDto)
        {
            return Ok(_service.Save(postTransactionDto));
        }
    }
}

