using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignIn
{
    public static class SignInCommandErrors
    {
        public static readonly Error InvalidCredentials = new($"SignIn.{nameof(InvalidCredentials)}", "Invalid credentials");
        public static readonly Error UserNotActive = new($"SignIn.{nameof(UserNotActive)}", "User not active");
    }
}
