using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace TemplateNetCore.Infrastructure.Data.MongoDb.Shared;

public static class Serializers
{
  public static GuidSerializer GuidSerializer() =>
      new(BsonType.String);

  public static DateTimeSerializer DateTimeSerializer() =>
      new(kind: DateTimeKind.Utc);

  public static NullableSerializer<DateTime> NullableDateTimeSerializer() =>
      new NullableSerializer<DateTime>().WithSerializer(DateTimeSerializer());

  public static DateTimeSerializer DateOnlySerializer() =>
      new(dateOnly: true);

  public static DecimalSerializer DecimalSerializer() =>
      new(BsonType.Decimal128);
}