using AutoMapper;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignUp;

public class SignUpCommandHandler : BaseCommandHandler<SignUpCommandHandler, SignUpCommand, SignUpCommandResponse>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    private readonly IHashService _hashService;

    public SignUpCommandHandler(
        ILogger<SignUpCommandHandler> logger,
        IUnityOfWork unityOfWork,
        IMapper mapper,
        IHashService hashService
    ) : base(logger)
    {
        _unityOfWork = unityOfWork;
        _mapper = mapper;
        _hashService = hashService;
    }

    public override async Task<Result<SignUpCommandResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var alreadyExists = (await _unityOfWork.UserRepository.GetByEmailAsync(request.Email)) != null;

            if (alreadyExists)
                return Failure(SignUpCommandErrors.UserAlreadyExists);

            var user = _mapper.Map<SignUpCommand, User>(request, opts => opts.AfterMap((_, dest) =>
            {
                dest.Password = _hashService.Hash(request.Password);
            }));

            await _unityOfWork.UserRepository.AddAsync(user);
            await _unityOfWork.CommitAsync();

            return Success(_mapper.Map<SignUpCommandResponse>(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating user {email}", request.Email);
            throw;
        }
    }
}
