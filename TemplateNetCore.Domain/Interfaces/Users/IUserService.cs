using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Domain.Interfaces.Users
{
    public interface IUserService
    {
        Task<User> GetById(Guid id);
        Guid GetLoggedUserId(ClaimsPrincipal claims);
        Task<GetLoginResponseDto> Login(PostLoginDto postLoginDto);
        Task SignUp(PostSignUpDto postSignUpDto);
    }
}
