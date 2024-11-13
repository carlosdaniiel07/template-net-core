using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.MongoDb.v1;
using TemplateNetCore.Infrastructure.Data.MongoDb.Mappings;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Repositories.v1;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(IConfiguration configuration)
        : base(configuration, new ProductMapping(), "Product")
    {

    }

    public async Task<IEnumerable<Product>> GetAllByCategoryAsync(string category)
    {
        return await _collection
            .Find(Builders<Product>.Filter.Eq(product => product.Category, category))
            .ToListAsync();
    }
}
