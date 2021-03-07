using System.Threading.Tasks;

using TemplateNetCore.Repository.Interfaces.Transactions;
using TemplateNetCore.Repository.Interfaces.Users;
using TemplateNetCore.Repository.EF.Repositories.Transactions;
using TemplateNetCore.Repository.EF.Repositories.Users;

namespace TemplateNetCore.Repository.EF
{
    public class UnitOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext context;

        private TransactionRepository _transactionRepository;
        private UserRepository _userRepository;

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

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(context);
                }

                return _userRepository;
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
