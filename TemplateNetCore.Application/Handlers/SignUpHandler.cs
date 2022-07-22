using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Commands.SignUp;
using TemplateNetCore.Domain.Interfaces.Users;

namespace TemplateNetCore.Application.Handlers
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpCommandResponse>
    {
        private readonly ILogger<SignUpHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public SignUpHandler(ILogger<SignUpHandler> logger, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<SignUpCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating a new user");

                var user = await _userService.SignUpAsync(request);

                _logger.LogInformation("User created successful");

                return _mapper.Map<SignUpCommandResponse>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on creating a new user");
                throw;
            }
        }
    }
}