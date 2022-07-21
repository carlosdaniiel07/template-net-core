using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateNetCore.Domain.Entities;
using TemplateNetCore.Repository.Interfaces;

namespace TemplateNetCore.Repository.EF.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        internal readonly ApplicationDbContext context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            return entities;
        }

        public bool Any(Expression<Func<T, bool>> expression = null)
        {
            return dbSet.Any(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null)
        {
            return await dbSet.AnyAsync(expression);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SoftDelete(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            dbSet.Update(entity);
        }

        public T Get(Expression<Func<T, bool>> expression = null)
        {
            return dbSet.FirstOrDefault(expression);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<T> GetAll(int take, Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query
                .AsNoTracking()
                .Take(take)
                .ToList();
        }

        public IEnumerable<T> GetAll(int skip, int take, Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[] includes)
        {
            IQueryable<T> query = dbSet;

            includes.ToList().ForEach(navigationProperty =>
            {
                query = query.Include(navigationProperty);
            });

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[] includes, Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = dbSet;

            includes.ToList().ForEach(navigationProperty =>
            {
                query = query.Include(navigationProperty);
            });

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int take, Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query
                .AsNoTracking()
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int skip, int take, Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null)
        {
            return await dbSet.FirstOrDefaultAsync(expression);
        }

        public T GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            dbSet.Update(entity);
        }
    }
}
