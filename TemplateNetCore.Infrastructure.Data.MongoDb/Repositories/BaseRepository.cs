using MongoDB.Driver;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.MongoDb;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly IMongoCollection<TEntity> _collection;

    protected BaseRepository(IMongoDatabase mongoDatabase, string collectionName) =>
        _collection = mongoDatabase.GetCollection<TEntity>(collectionName);

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.DeletedAt, null);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var builder = Builders<TEntity>.Filter;
        var filter = builder.And(
            builder.Eq(x => x.Id, id),
            builder.Eq(x => x.DeletedAt, null)
        );

        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }
}
