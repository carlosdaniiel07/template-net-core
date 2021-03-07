using System;

using TemplateNetCore.Domain.Enums.Users;

namespace TemplateNetCore.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
    }
}
