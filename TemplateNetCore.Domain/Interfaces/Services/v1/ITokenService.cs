using TemplateNetCore.Domain.Entities.v1;

namespace TemplateNetCore.Domain.Interfaces.Services.v1;

public interface ITokenService
{
    string Generate(User user);
}
