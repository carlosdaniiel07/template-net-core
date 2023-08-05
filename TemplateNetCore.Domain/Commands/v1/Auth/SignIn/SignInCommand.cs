using MediatR;

namespace TemplateNetCore.Domain.Commands.v1.Auth.SignIn
{
    public class SignInCommand : IRequest<SignInCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
