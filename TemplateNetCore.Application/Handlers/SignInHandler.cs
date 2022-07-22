using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Commands.SignIn;
using TemplateNetCore.Domain.Interfaces.Users;

namespace TemplateNetCore.Application.Handlers
{
    public class SignInHandler : IRequestHandler<SignInCommand, SignInCommandResponse>
    {
        private readonly ILogger<SignInHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public SignInHandler(ILogger<SignInHandler> logger, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<SignInCommandResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Authenticating user");

                var accessToken = await _userService.SignInAsync(request);

                _logger.LogInformation("User authentication successful");

                return new SignInCommandResponse
                {
                    AccessToken = accessToken,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on authenticating user");
                throw;
            }
        }
    }
}