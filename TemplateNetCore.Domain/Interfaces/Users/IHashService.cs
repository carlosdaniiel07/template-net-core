namespace TemplateNetCore.Domain.Interfaces.Users
{
    public interface IHashService
    {
        string Hash(string value);
        bool Compare(string hash, string value);
    }
}
