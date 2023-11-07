using MediatR;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Commands.v1.Auth.SignIn;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignIn
{
    public class SignInCommandHandler : BaseCommandHandler<SignInCommandHandler>, IRequestHandler<SignInCommand, Result<SignInCommandResponse>>
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public SignInCommandHandler(
            ILogger<SignInCommandHandler> logger,
            IUnityOfWork unityOfWork,
            IHashService hashService,
            ITokenService tokenService
        ) : base(logger)
        {
            _unityOfWork = unityOfWork;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<Result<SignInCommandResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Authenticating user {email}", request.Email);

                var user = await _unityOfWork.UserRepository.GetByEmailAsync(request.Email);

                if (user == null)
                    return Result<SignInCommandResponse>.Failure(SignInCommandErrors.InvalidCredentials);

                var isValidPassword = _hashService.Compare(user.Password, request.Password);

                if (!isValidPassword)
                    return Result<SignInCommandResponse>.Failure(SignInCommandErrors.InvalidCredentials);

                if (!user.Active)
                    return Result<SignInCommandResponse>.Failure(SignInCommandErrors.UserNotActive);

                var accessToken = _tokenService.Generate(user);
                var refreshToken = Guid.NewGuid().ToString();

                return Result<SignInCommandResponse>.Success(new SignInCommandResponse(accessToken, refreshToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while authenticating user @email", request.Email);
                throw;
            }
        }
    }
}
