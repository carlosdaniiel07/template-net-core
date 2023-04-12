using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Infrastructure.Service.Services.v1
{
    public class JwtTokenService : ITokenService
    {
        public string Generate(User user)
        {
            return $"accessToken:{user.Id}";
        }
    }
}
