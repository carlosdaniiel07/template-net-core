using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Repository.Interfaces.Transactions;

namespace TemplateNetCore.Repository.EF.Repositories.Transactions
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
