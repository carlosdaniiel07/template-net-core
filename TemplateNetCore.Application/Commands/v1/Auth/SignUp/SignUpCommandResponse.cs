namespace TemplateNetCore.Application.Commands.v1.Auth.SignUp;

public class SignUpCommandResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
}
