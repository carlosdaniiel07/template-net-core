using MediatR;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Commands.v1.Auth.SignIn;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignIn
{
    public class SignInCommandHandler : BaseCommandHandler<SignInCommandHandler>, IRequestHandler<SignInCommand, SignInCommandResponse>
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public SignInCommandHandler(
            ILogger<SignInCommandHandler> logger,
            INotificationContextService notificationContextService,
            IUnityOfWork unityOfWork,
            IHashService hashService,
            ITokenService tokenService
        ) : base(logger, notificationContextService)
        {
            _unityOfWork = unityOfWork;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<SignInCommandResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Authenticating user {email}", request.Email);

                var user = await _unityOfWork.UserRepository.GetByEmailAsync(request.Email);

                if (user == null)
                {
                    AddNotification(SignInCommandErrors.InvalidCredentials);
                    return default;
                }

                var isValidPassword = _hashService.Compare(user.Password, request.Password);

                if (!isValidPassword)
                {
                    AddNotification(SignInCommandErrors.InvalidCredentials);
                    return default;
                }

                if (!user.Active)
                {
                    AddNotification(SignInCommandErrors.UserNotActive);
                    return default;
                }

                var accessToken = _tokenService.Generate(user);
                var refreshToken = Guid.NewGuid().ToString();

                return new SignInCommandResponse(accessToken, refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while authenticating user @email", request.Email);
                throw;
            }
        }
    }
}
