using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Enums.Transactions;
using TemplateNetCore.Domain.Interfaces.Transactions;

using TemplateNetCore.Repository;

namespace TemplateNetCore.Service.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnityOfWork _unityOfWork;

        public TransactionService(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _unityOfWork.TransactionRepository.GetAllAsync();
        }

        public async Task<Transaction> Save(Transaction transaction)
        {
            transaction.Status = TransactionStatus.Pending;

            await _unityOfWork.TransactionRepository.AddAsync(transaction);
            await _unityOfWork.CommitAsync();

            return transaction;
        }
    }
}
