using MongoDB.Bson.Serialization;
using TemplateNetCore.Infrastructure.Data.MongoDb.Mappings;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Shared;

public static class MongoDbConfiguration
{
    public static void Configure()
    {
        RegisterClassMaps();
        RegisterSerializers();
    }

    private static void RegisterClassMaps()
    {
        var mappingsAssembly = typeof(BaseEntityMapping).Assembly;
        var mappings = mappingsAssembly
            .GetTypes()
            .Where(type => type.IsClass && typeof(IMapping).IsAssignableFrom(type))
            .Select(type => Activator.CreateInstance(type) as IMapping)
            .ToList();

        foreach (var mapping in mappings)
            mapping.RegisterClassMap();
    }

    private static void RegisterSerializers()
    {
        BsonSerializer.RegisterSerializer(Serializers.GuidSerializer());
    }
}