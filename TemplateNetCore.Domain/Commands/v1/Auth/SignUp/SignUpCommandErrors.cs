using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Domain.Commands.v1.Auth.SignUp
{
    public static class SignUpCommandErrors
    {
        public static readonly Error UserAlreadyExists = new ($"SignUp.{nameof(UserAlreadyExists)}", "User already exists");
    }
}
