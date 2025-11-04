using TemplateNetCore.Domain.Interfaces.ServiceClients;
using TemplateNetCore.Domain.Models.v1.ServiceClients.ZipCode.Find;
using TemplateNetCore.Infrastructure.Service.Services.v1;

namespace TemplateNetCore.Infrastructure.Service.ServiceClients.v1;

public class ViaCepServiceClient(HttpClient httpClient) : HttpService(httpClient), IZipCodeServiceClient
{
    public async Task<FindResponseModel> FindAsync(FindRequestModel request) =>
        await GetAsync<FindResponseModel>($"/ws/{request.ZipCode}/json");
}