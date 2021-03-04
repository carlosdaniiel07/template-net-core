using TemplateNetCore.Domain.Dto.Transactions;
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

        public Transaction Save(PostTransactionDto postTransactionDto)
        {
            var transaction = new Transaction
            {
                Date = postTransactionDto.Date,
                Description = postTransactionDto.Description,
                Value = postTransactionDto.Value,
                TargetKey = postTransactionDto.TargetKey,
                Status = TransactionStatus.Pending
            };

            _unityOfWork.TransactionRepository.Add(transaction);
            _unityOfWork.Commit();

            return transaction;
        }
    }
}
