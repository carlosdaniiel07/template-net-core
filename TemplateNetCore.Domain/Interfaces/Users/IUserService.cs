using System.Threading.Tasks;
using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Domain.Interfaces.Users
{
    public interface IUserService
    {
        Task<GetLoginResponseDto> Login(PostLoginDto postLoginDto);
        Task SignUp(User user);
    }
}
