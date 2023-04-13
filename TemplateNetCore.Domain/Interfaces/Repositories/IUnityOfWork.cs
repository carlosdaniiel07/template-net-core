using TemplateNetCore.Domain.Interfaces.Repositories.v1;

namespace TemplateNetCore.Domain.Interfaces.Repositories
{
    public interface IUnityOfWork
    {
        IUserRepository UserRepository { get; }
        void Commit();
        Task CommitAsync();
    }
}
