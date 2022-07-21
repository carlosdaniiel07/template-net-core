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
        void Add(T t);
        void Update(T t);
        void Delete(T t);
        void SoftDelete(T t);
        void AddRange(IEnumerable<T> ts);
        Task<IEnumerable<T>> GetAllAsync(string[] includes);
        Task<IEnumerable<T>> GetAllAsync(string[] includes, Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetAllAsync(int take, Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetAllAsync(int skip, int take, Expression<Func<T, bool>> expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null);
        Task<T> GetByIdAsync(Guid id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null);
        Task AddAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> ts);
    }
}
