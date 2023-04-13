namespace TemplateNetCore.Domain.UseCases.v1.Auth.SignIn
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
