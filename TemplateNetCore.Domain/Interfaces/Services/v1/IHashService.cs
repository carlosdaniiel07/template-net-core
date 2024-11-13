namespace TemplateNetCore.Domain.Interfaces.Services.v1;

public interface IHashService
{
    string Hash(string value);
    bool Compare(string hash, string value);
}
