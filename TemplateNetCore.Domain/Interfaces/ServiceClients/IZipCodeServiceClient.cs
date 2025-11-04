using TemplateNetCore.Domain.Models.v1.ServiceClients.ZipCode.Find;

namespace TemplateNetCore.Domain.Interfaces.ServiceClients;

public interface IZipCodeServiceClient
{
    Task<FindResponseModel> FindAsync(FindRequestModel request);
}