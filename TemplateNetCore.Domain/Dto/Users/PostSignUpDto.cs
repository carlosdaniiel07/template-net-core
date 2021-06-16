using TemplateNetCore.Domain.Enums.Users;
using System.ComponentModel.DataAnnotations;

namespace TemplateNetCore.Domain.Dto.Users
{
    public class PostSignUpDto
    {
        [Required]
        public string Name{ get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
    }
}
