using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Infrastructure.Service.Services.v1
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient) =>
            _httpClient = httpClient;

        public async Task<TResponse> GetAsync<TResponse>(string url, Dictionary<string, string> headers = null)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            AddHeaders(requestMessage, headers);

            var response = await _httpClient.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, Dictionary<string, string> headers = null)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, new MediaTypeHeaderValue(MediaTypeNames.Application.Json));
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content,
            };

            AddHeaders(requestMessage, headers);

            var response = await _httpClient.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        private void AddHeaders(HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers)
        {
            foreach (var header in headers ?? new Dictionary<string, string>())
                httpRequestMessage.Headers.Add(header.Key, header.Value);
        }
    }
}
