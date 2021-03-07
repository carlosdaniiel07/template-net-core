using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Domain.Interfaces.Users
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}
