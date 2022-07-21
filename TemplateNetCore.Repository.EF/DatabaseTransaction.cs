using Microsoft.EntityFrameworkCore.Storage;
using TemplateNetCore.Repository.Interfaces;

namespace TemplateNetCore.Repository.EF
{
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public DatabaseTransaction(ApplicationDbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void Commit() =>
            _transaction.Commit();

        public async Task CommitAsync() =>
            await _transaction.CommitAsync();

        public void Rollback() =>
            _transaction.Rollback();

        public async Task RollbackAsync() =>
            await _transaction.RollbackAsync();

        public void Dispose() =>
            _transaction.Dispose();
    }
}