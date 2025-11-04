using MongoDB.Driver;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.MongoDb.v1;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Repositories.v1;

public class ProductRepository(IMongoDatabase mongoDatabase) : BaseRepository<Product>(mongoDatabase, "product"), IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllByCategoryAsync(string category)
    {
        var filter = Builders<Product>.Filter.Eq(product => product.Category, category);
        return await _collection.Find(filter).ToListAsync();
    }
}
