using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Dto.Transactions;

namespace TemplateNetCore.Domain.Interfaces.Transactions
{
    public interface ITransactionService
    {
        Transaction Save(PostTransactionDto postTransactionDto);
    }
}
