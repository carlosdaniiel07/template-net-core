using TemplateNetCore.Repository.Interfaces;
using TemplateNetCore.Repository.Interfaces.Transactions;
using TemplateNetCore.Repository.Interfaces.Users;

namespace TemplateNetCore.Repository
{
    public interface IUnityOfWork
    {
        ITransactionRepository TransactionRepository { get; }
        IUserRepository UserRepository { get; }
        void Commit();
        Task CommitAsync();
        IDatabaseTransaction BeginTransaction();
    }
}
