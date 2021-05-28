using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public void Add(T t)
        {
            dbSet.Add(t);
        }

        public async Task AddAsync(T t)
        {
            await dbSet.AddAsync(t);
        }

        public void AddRange(IEnumerable<T> ts)
        {
            dbSet.AddRange(ts);
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> ts)
        {
            await dbSet.AddRangeAsync(ts);
            return ts;
        }

        public bool Any(Expression<Func<T, bool>> expression = null)
        {
            return dbSet.Any(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null)
        {
            return await dbSet.AnyAsync(expression);
        }

        public void Delete(T t)
        {
            dbSet.Remove(t);
        }

        public Task DeleteAsync(T t)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(T t)
        {
            t.DeletedAt = DateTime.Now;
            dbSet.Update(t);
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

        public void Update(T t)
        {
            dbSet.Update(t);
        }

        public Task UpdateAsync(T t)
        {
            throw new NotImplementedException();
        }
    }
}
