using TemplateNetCore.Domain.Commands.CreateTransaction;
using TemplateNetCore.Domain.Entities.Transactions;

namespace TemplateNetCore.Domain.Interfaces.Transactions
{
    public interface ITransactionService
    {
        Task<Transaction> SaveAsync(CreateTransactionCommand command);
    }
}
