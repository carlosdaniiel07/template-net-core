namespace TemplateNetCore.Domain.Entities.v1;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; } = true;
}
