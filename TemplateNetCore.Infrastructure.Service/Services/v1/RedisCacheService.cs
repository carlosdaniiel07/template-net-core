using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Infrastructure.Service.Services.v1
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RedisCacheService(IConfiguration configuration)
        {
            var connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));

            _database = connection.GetDatabase();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task AddAsync<T>(string key, T value, TimeSpan? ttl)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);

            var json = JsonSerializer.Serialize(value, _jsonSerializerOptions);

            await _database.StringSetAsync(key, json, ttl);
        }

        public async Task<T> RetrieveAsync<T>(string key)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);

            var value = await _database.StringGetAsync(key);

            if (!value.HasValue)
                return default;

            return JsonSerializer.Deserialize<T>(value.ToString(), _jsonSerializerOptions);
        }

        public async Task DeleteAsync(string key)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);
            await _database.KeyDeleteAsync(key);
        }
    }
}
