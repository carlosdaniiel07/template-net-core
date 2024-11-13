using MongoDB.Bson.Serialization;
using TemplateNetCore.Domain.Entities.v1;

namespace TemplateNetCore.Infrastructure.Data.MongoDb;

public interface IMapping<TEntity> where TEntity : BaseEntity
{
    public BsonClassMap<TEntity> RegisterMap();
}
