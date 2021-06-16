using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Enums.Transactions;
using TemplateNetCore.Domain.Interfaces.Transactions;
using TemplateNetCore.Domain.Interfaces.Users;
using TemplateNetCore.Repository;

namespace TemplateNetCore.Service.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IUserService _userService;

        public TransactionService(IUnityOfWork unityOfWork, IUserService userService)
        {
            _unityOfWork = unityOfWork;
            _userService = userService;
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _unityOfWork.TransactionRepository.GetAllAsync(new string[] { "User" });
        }

        public async Task<Transaction> Save(Guid userId, Transaction transaction)
        {
            transaction.Status = TransactionStatus.Pending;
            transaction.User = await _userService.GetById(userId);

            await _unityOfWork.TransactionRepository.AddAsync(transaction);
            await _unityOfWork.CommitAsync();

            return transaction;
        }
    }
}
