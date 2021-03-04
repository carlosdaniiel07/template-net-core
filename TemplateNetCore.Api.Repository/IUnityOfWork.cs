using System.Threading.Tasks;

using TemplateNetCore.Repository.Interfaces.Transactions;

namespace TemplateNetCore.Repository
{
    public interface IUnityOfWork
    {
        ITransactionRepository TransactionRepository { get; }
        
        void Commit();
        Task CommitAsync();
    }
}
