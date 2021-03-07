using TemplateNetCore.Domain.Enums.Users;

namespace TemplateNetCore.Domain.Dto.Users
{
    public class PostSignUpDto
    {
        public string Name{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
