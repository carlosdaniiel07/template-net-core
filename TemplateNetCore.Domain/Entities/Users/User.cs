using TemplateNetCore.Domain.Enums.Users;

namespace TemplateNetCore.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }

        public void Create(string passwordHash)
        {
            Password = passwordHash;
            Role = UserRole.Default;
            IsActive = true;
        }
    }
}
