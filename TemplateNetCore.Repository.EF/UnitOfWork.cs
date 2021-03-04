using System.Threading.Tasks;

using TemplateNetCore.Repository.Interfaces.Transactions;
using TemplateNetCore.Repository.EF.Repositories.Transactions;

namespace TemplateNetCore.Repository.EF
{
    public class UnitOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext context;

        private TransactionRepository _transactionRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new TransactionRepository(context);
                }

                return _transactionRepository;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
