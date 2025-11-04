using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Infrastructure.Data.MongoDb.Shared;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Mappings;

public class BaseEntityMapping : IMapping
{
    public void RegisterClassMap()
    {
        BsonClassMap.RegisterClassMap<BaseEntity>(classMap =>
        {
            classMap.SetIgnoreExtraElements(true);

            classMap
                .MapIdMember(x => x.Id)
                .SetSerializer(Serializers.GuidSerializer())
                .SetIdGenerator(GuidGenerator.Instance)
                .SetElementName("id");

            classMap
                .MapMember(x => x.CreatedAt)
                .SetSerializer(Serializers.DateTimeSerializer())
                .SetElementName("createdAt");

            classMap
                .MapMember(x => x.UpdatedAt)
                .SetSerializer(Serializers.NullableDateTimeSerializer())
                .SetElementName("updatedAt");

            classMap
                .MapMember(x => x.DeletedAt)
                .SetSerializer(Serializers.NullableDateTimeSerializer())
                .SetElementName("deletedAt");
        });
    }
}
