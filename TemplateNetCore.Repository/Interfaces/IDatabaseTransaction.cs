namespace TemplateNetCore.Repository.Interfaces
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}