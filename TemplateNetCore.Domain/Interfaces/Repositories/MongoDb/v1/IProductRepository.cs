using TemplateNetCore.Domain.Entities.v1;

namespace TemplateNetCore.Domain.Interfaces.Repositories.MongoDb.v1
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllByCategoryAsync(string category);
    }
}
