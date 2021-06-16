using System;
using System.Security.Claims;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Domain.Interfaces.Users
{
    public interface ITokenService
    {
        string Generate(User user);
        Guid GetIdByClaims(ClaimsPrincipal claims);
    }
}
