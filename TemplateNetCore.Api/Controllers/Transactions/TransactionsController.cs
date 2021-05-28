using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTransactionDto>>> GetAll()
        {
            var transactions = _mapper.Map<IEnumerable<GetTransactionDto>>(await _service.GetAll());
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> Save([FromBody] PostTransactionDto postTransactionDto)
        {
            var transaction = _mapper.Map<Transaction>(postTransactionDto);
            return Ok(await _service.Save(transaction));
        }
    }
}

