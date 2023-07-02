using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Infrastructure.Service.Services.v1
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;

        public RedisCacheService(IConfiguration configuration)
        {
            var connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            _database = connection.GetDatabase();
        }

        public async Task AddAsync<T>(string key, T value, TimeSpan ttl)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var json = JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            await _database.StringSetAsync(key, json, ttl);
        }

        public async Task<T> RetrieveAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var value = await _database.StringGetAsync(key);

            if (!value.HasValue)
                return default;

            return JsonSerializer.Deserialize<T>(value.ToString(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        public async Task DeleteAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            await _database.KeyDeleteAsync(key);
        }
    }
}
