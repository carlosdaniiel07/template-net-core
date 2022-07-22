using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Commands.CreateTransaction;
using TemplateNetCore.Domain.Interfaces.Transactions;

namespace TemplateNetCore.Application.Handlers
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionCommandResponse>
    {
        private readonly ILogger<CreateTransactionHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public CreateTransactionHandler(ILogger<CreateTransactionHandler> logger, IMapper mapper, ITransactionService transactionService)
        {
            _logger = logger;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        public async Task<CreateTransactionCommandResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating transaction");

                var transaction = await _transactionService.SaveAsync(request);

                _logger.LogInformation("Transaction created successful");

                return _mapper.Map<CreateTransactionCommandResponse>(transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on creating transaction");
                throw;
            }
        }
    }
}