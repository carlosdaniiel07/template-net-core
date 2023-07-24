using System.ComponentModel;

namespace TemplateNetCore.Domain.UseCases.v1.Auth.SignIn
{
    public enum SignInErrors
    {
        [Description("INVALID_CREDENTIALS")]
        InvalidCredentials = 1,

        [Description("USER_NOT_ACTIVE")]
        UserNotActive = 2,
    }
}
