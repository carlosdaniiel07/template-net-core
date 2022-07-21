using TemplateNetCore.Domain.Dto.Transactions;
using TemplateNetCore.Domain.Entities.Transactions;

namespace TemplateNetCore.Domain.Interfaces.Transactions
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAll();
        Task<Transaction> Save(Guid userId, PostTransactionDto postTransactionDto);
    }
}
