using MongoDB.Bson.Serialization;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Infrastructure.Data.MongoDb.Shared;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Mappings;

public class ProductMapping : IMapping
{
    public void RegisterClassMap()
    {
        BsonClassMap.RegisterClassMap<Product>(classMap =>
        {
            classMap
                .MapMember(x => x.Description)
                .SetElementName("description");

            classMap
                .MapMember(x => x.Category)
                .SetElementName("category");

            classMap
                .MapMember(x => x.Value)
                .SetSerializer(Serializers.DecimalSerializer())
                .SetElementName("value");
        });
    }
}
