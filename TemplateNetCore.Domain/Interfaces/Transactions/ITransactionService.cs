using System.Collections.Generic;

using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Dto.Transactions;

namespace TemplateNetCore.Domain.Interfaces.Transactions
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll();
        Transaction Save(PostTransactionDto postTransactionDto);
    }
}
