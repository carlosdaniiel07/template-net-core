using System.Linq.Expressions;
using TemplateNetCore.Domain.Entities;

namespace TemplateNetCore.Repository.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null);
        IEnumerable<T> GetAll(int take, Expression<Func<T, bool>> expression = null);
        IEnumerable<T> GetAll(int skip, int take, Expression<Func<T, bool>> expression = null);
        T Get(Expression<Func<T, bool>> expression = null);
        T GetById(Guid id);
        bool Any(Expression<Func<T, bool>> expression = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync(string[] includes);
        Task<IEnumerable<T>> GetAllAsync(string[] includes, Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetAllAsync(int take, Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetAllAsync(int skip, int take, Expression<Func<T, bool>> expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null);
        Task<T> GetByIdAsync(Guid id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null);
        Task AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    }
}
