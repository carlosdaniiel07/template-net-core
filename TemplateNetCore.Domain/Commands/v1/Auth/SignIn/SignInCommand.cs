using MediatR;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Domain.Commands.v1.Auth.SignIn
{
    public class SignInCommand : IRequest<Result<SignInCommandResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
