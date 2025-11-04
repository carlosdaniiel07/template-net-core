using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignIn;

public class SignInCommandHandler(
    ILogger<SignInCommandHandler> logger,
    IUnityOfWork unityOfWork,
    IHashService hashService,
    ITokenService tokenService
    ) : BaseCommandHandler<SignInCommandHandler, SignInCommand, SignInCommandResponse>
{
    public override async Task<Result<SignInCommandResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Authenticating user {email}", request.Email);

            var user = await unityOfWork.UserRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return Failure(SignInCommandErrors.InvalidCredentials);

            var isValidPassword = hashService.Compare(user.Password, request.Password);

            if (!isValidPassword)
                return Failure(SignInCommandErrors.InvalidCredentials);

            if (!user.Active)
                return Failure(SignInCommandErrors.UserNotActive);

            var accessToken = tokenService.Generate(user);
            var refreshToken = Guid.NewGuid().ToString();

            return Success(new SignInCommandResponse(accessToken, refreshToken));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while authenticating user {email}", request.Email);
            throw;
        }
    }
}
