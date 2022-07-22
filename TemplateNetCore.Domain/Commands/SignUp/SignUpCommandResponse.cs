using TemplateNetCore.Domain.Enums.Users;

namespace TemplateNetCore.Domain.Commands.SignUp
{
    public class SignUpCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}