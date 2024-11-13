using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TemplateNetCore.Domain.Entities.v1;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Mappings
{
    public class ProductMapping : IMapping<Product>
    {
        public BsonClassMap<Product> RegisterMap()
        {
            return BsonClassMap.RegisterClassMap<Product>(classMap =>
            {
                classMap
                    .MapMember(survey => survey.Description)
                    .SetElementName("description");

                classMap
                    .MapMember(survey => survey.Category)
                    .SetElementName("category");

                classMap
                    .MapMember(survey => survey.Value)
                    .SetSerializer(new DecimalSerializer(BsonType.Decimal128))
                    .SetElementName("value");
            });
        }
    }
}
