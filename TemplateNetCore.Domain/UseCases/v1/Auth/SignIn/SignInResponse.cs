namespace TemplateNetCore.Domain.UseCases.v1.Auth.SignIn
{
    public class SignInResponse
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }

        public SignInResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
