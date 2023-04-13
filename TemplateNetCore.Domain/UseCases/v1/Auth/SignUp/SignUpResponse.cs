namespace TemplateNetCore.Domain.UseCases.v1.Auth.SignUp
{
    public class SignUpResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; } = true;
    }
}
