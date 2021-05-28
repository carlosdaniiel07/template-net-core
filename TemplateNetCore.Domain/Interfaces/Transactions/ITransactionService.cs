using System.Collections.Generic;
using System.Threading.Tasks;

using TemplateNetCore.Domain.Entities.Transactions;

namespace TemplateNetCore.Domain.Interfaces.Transactions
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAll();
        Task<Transaction> Save(Transaction transaction);
    }
}
