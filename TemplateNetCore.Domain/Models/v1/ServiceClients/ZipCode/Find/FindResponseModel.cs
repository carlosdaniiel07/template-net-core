using System.Text.Json.Serialization;

namespace TemplateNetCore.Domain.Models.v1.ServiceClients.ZipCode.Find;

public class FindResponseModel
{
    [JsonPropertyName("cep")]
    public string ZipCode { get; set; }

    [JsonPropertyName("bairro")]
    public string Neighborhood { get; set; }

    [JsonPropertyName("localidade")]
    public string City { get; set; }

    [JsonPropertyName("uf")]
    public string State { get; set; }
}