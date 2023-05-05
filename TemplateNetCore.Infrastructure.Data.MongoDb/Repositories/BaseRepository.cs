using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.MongoDb;
using TemplateNetCore.Infrastructure.Data.MongoDb.Mappings;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IMongoCollection<TEntity> _collection;

        protected BaseRepository(IConfiguration configuration, IMapping<TEntity> mapping, string collectionName)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var databaseName = connectionString.Split('/').LastOrDefault();

            BsonClassMap.TryRegisterClassMap(BaseEntityMapping.GetClassMap());
            mapping.RegisterMap();

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);

            _collection = mongoDatabase.GetCollection<TEntity>(collectionName);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(TEntity entity) =>
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);

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

        public async Task UpdateAsync(TEntity entity) =>
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }
}
