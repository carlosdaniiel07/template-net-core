using System.ComponentModel;

namespace TemplateNetCore.Domain.Commands.v1.Auth.SignIn
{
    public enum SignInCommandErrors
    {
        [Description("INVALID_CREDENTIALS")]
        InvalidCredentials = 1,

        [Description("USER_NOT_ACTIVE")]
        UserNotActive = 2,
    }
}
