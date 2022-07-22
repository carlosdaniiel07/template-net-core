using MediatR;

namespace TemplateNetCore.Domain.Commands.SignIn
{
    public class SignInCommand : IRequest<SignInCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}