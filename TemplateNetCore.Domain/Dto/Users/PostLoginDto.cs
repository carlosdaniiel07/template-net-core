using System.ComponentModel.DataAnnotations;

namespace TemplateNetCore.Domain.Dto.Users
{
    public class PostLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
