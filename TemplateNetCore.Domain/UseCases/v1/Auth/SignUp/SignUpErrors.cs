using System.ComponentModel;

namespace TemplateNetCore.Domain.UseCases.v1.Auth.SignUp
{
    public enum SignUpErrors
    {
        [Description("USER_ALREADY_EXISTS")]
        UserAlreadyExists = 1,
    }
}
