using System.ComponentModel;

namespace TemplateNetCore.Domain.Commands.v1.Auth.SignUp
{
    public enum SignUpCommandErrors
    {
        [Description("USER_ALREADY_EXISTS")]
        UserAlreadyExists = 1,
    }
}
