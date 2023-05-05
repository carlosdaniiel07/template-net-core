using Microsoft.EntityFrameworkCore;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;

namespace TemplateNetCore.Infrastructure.Data.Sql.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        internal readonly ApplicationDbContext _context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await dbSet
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            dbSet.Update(entity);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            dbSet.Update(entity);

            await Task.CompletedTask;
        }
    }
}
