using TemplateNetCore.Repository.Interfaces.Transactions;
using TemplateNetCore.Repository.Interfaces.Users;
using TemplateNetCore.Repository.EF.Repositories.Transactions;
using TemplateNetCore.Repository.EF.Repositories.Users;
using TemplateNetCore.Repository.Interfaces;

namespace TemplateNetCore.Repository.EF
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext _context;
        private TransactionRepository _transactionRepository;
        private UserRepository _userRepository;

        public UnityOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                    _transactionRepository = new TransactionRepository(_context);

                return _transactionRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);

                return _userRepository;
            }
        }

        public void Commit() =>
            _context.SaveChanges();

        public async Task CommitAsync() =>
            await _context.SaveChangesAsync();

        public IDatabaseTransaction BeginTransaction() =>
            new DatabaseTransaction(_context);
    }
}
