using TemplateNetCore.Domain.Commands.SignIn;
using TemplateNetCore.Domain.Commands.SignUp;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Domain.Interfaces.Users
{
    public interface IUserService
    {
        Task<User> SignUpAsync(SignUpCommand command);
        Task<string> SignInAsync(SignInCommand command);
        Task<User> GetLoggedUserAsync();
    }
}
