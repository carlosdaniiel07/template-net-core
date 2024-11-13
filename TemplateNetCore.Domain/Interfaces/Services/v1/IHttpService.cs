namespace TemplateNetCore.Domain.Interfaces.Services.v1;

public interface IHttpService
{
    Task<TResponse> GetAsync<TResponse>(string url, Dictionary<string, string> headers = null);
    Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, Dictionary<string, string> headers = null);
}
