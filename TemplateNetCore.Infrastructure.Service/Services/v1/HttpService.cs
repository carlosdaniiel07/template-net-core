using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace TemplateNetCore.Infrastructure.Service.Services.v1;

public abstract class HttpService(HttpClient httpClient)
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
    };

    public async Task<TResponse> GetAsync<TResponse>(string url, Dictionary<string, string> headers = null)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

        AddHeaders(requestMessage, headers);

        var response = await httpClient.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TResponse>(responseJson, _jsonSerializerOptions);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, Dictionary<string, string> headers = null)
    {
        var content = new StringContent(JsonSerializer.Serialize(request, _jsonSerializerOptions), Encoding.UTF8, new MediaTypeHeaderValue(MediaTypeNames.Application.Json));
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = content,
        };

        AddHeaders(requestMessage, headers);

        var response = await httpClient.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TResponse>(responseJson, _jsonSerializerOptions);
    }

    private void AddHeaders(HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers)
    {
        foreach (var header in headers ?? [])
            httpRequestMessage.Headers.Add(header.Key, header.Value);
    }
}
