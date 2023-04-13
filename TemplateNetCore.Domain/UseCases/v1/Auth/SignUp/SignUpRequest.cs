namespace TemplateNetCore.Domain.UseCases.v1.Auth.SignUp
{
    public class SignUpRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
