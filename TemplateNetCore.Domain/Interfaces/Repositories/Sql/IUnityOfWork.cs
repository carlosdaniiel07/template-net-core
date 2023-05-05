using TemplateNetCore.Domain.Interfaces.Repositories.Sql.v1;

namespace TemplateNetCore.Domain.Interfaces.Repositories.Sql
{
    public interface IUnityOfWork
    {
        IUserRepository UserRepository { get; }
        void Commit();
        Task CommitAsync();
    }
}
