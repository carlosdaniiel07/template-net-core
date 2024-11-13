namespace TemplateNetCore.Application.Commands.v1.Auth.SignIn;

public class SignInCommandResponse
{
    public string AccessToken { get; private set; }
    public string RefreshToken { get; private set; }

    public SignInCommandResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
