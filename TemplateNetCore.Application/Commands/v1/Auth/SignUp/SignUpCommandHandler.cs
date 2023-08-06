﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Commands.v1.Auth.SignUp;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignUp
{
    public class SignUpCommandHandler : BaseCommandHandler<SignUpCommandHandler>, IRequestHandler<SignUpCommand, SignUpCommandResponse>
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;

        public SignUpCommandHandler(
            ILogger<SignUpCommandHandler> logger,
            INotificationContextService notificationContextService,
            IUnityOfWork unityOfWork,
            IMapper mapper,
            IHashService hashService
        ) : base(logger, notificationContextService)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _hashService = hashService;
        }

        public async Task<SignUpCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var alreadyExists = (await _unityOfWork.UserRepository.GetByEmailAsync(request.Email)) != null;

                if (alreadyExists)
                {
                    AddNotification(SignUpCommandErrors.UserAlreadyExists);
                    return default;
                }

                var user = _mapper.Map<User>(request);

                user.Password = _hashService.Hash(request.Password);

                await _unityOfWork.UserRepository.AddAsync(user);
                await _unityOfWork.CommitAsync();

                return _mapper.Map<SignUpCommandResponse>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating user @email", request.Email);
                throw;
            }
        }
    }
}