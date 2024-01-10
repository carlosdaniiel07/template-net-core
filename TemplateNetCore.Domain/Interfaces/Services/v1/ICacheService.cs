namespace TemplateNetCore.Domain.Interfaces.Services.v1
{
    public interface ICacheService
    {
        Task<T> RetrieveAsync<T>(string key);
        Task AddAsync<T>(string key, T value, TimeSpan? ttl);
        Task DeleteAsync(string key);
    }
}
