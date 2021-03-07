using TemplateNetCore.Domain.Dto.Users;

namespace TemplateNetCore.Domain.Interfaces.Users
{
    public interface IUserService
    {
        GetLoginResponseDto Login(PostLoginDto postLoginDto);
        void SignUp(PostSignUpDto signUpDto);
    }
}
