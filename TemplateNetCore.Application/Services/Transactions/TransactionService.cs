using AutoMapper;
using TemplateNetCore.Application.Exceptions;
using TemplateNetCore.Domain.Commands.CreateTransaction;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Interfaces.Transactions;
using TemplateNetCore.Domain.Interfaces.Users;
using TemplateNetCore.Domain.Models;
using TemplateNetCore.Repository;

namespace TemplateNetCore.Application.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public TransactionService(IUnityOfWork unityOfWork, IMapper mapper, IUserService userService)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Transaction> SaveAsync(CreateTransactionCommand command)
        {
             var validator = new Validator<CreateTransactionCommand, CreateTransactionCommandValidator>(command);

            if (!validator.IsValid())
                throw new ValidationException(validator.GetFirstError());

            var loggedUser = await _userService.GetLoggedUserAsync();
            var transaction = _mapper.Map<Transaction>(command);

            transaction.Create(loggedUser);

            await _unityOfWork.TransactionRepository.AddAsync(transaction);
            await _unityOfWork.CommitAsync();

            return transaction;
        }
    }
}
