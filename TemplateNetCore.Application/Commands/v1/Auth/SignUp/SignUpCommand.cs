using MediatR;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignUp
{
    public class SignUpCommand : IRequest<Result<SignUpCommandResponse>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
