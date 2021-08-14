using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateNetCore.Domain.Dto.Transactions;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Interfaces.Transactions;
using TemplateNetCore.Domain.Interfaces.Users;

namespace TemplateNetCore.Api.Controllers.Transactions
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionsController(ITransactionService service, IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
            var userId = _userService.GetLoggedUserId(_httpContextAccessor.HttpContext.User);
            var transaction = await _service.Save(userId, postTransactionDto);

            return Ok(_mapper.Map<GetTransactionDto>(transaction));
        }
    }
}

