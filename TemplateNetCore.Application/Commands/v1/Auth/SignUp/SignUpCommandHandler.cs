using AutoMapper;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignUp;

public class SignUpCommandHandler(
    ILogger<SignUpCommandHandler> logger,
    IUnityOfWork unityOfWork,
    IMapper mapper,
    IHashService hashService) : BaseCommandHandler<SignUpCommandHandler, SignUpCommand, SignUpCommandResponse>
{
    public override async Task<Result<SignUpCommandResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var alreadyExists = (await unityOfWork.UserRepository.GetByEmailAsync(request.Email)) != null;

            if (alreadyExists)
                return Failure(SignUpCommandErrors.UserAlreadyExists);

            var user = mapper.Map<SignUpCommand, User>(request, opts => opts.AfterMap((_, dest) =>
            {
                dest.Password = hashService.Hash(request.Password);
            }));

            await unityOfWork.UserRepository.AddAsync(user);
            await unityOfWork.CommitAsync();

            return Success(mapper.Map<SignUpCommandResponse>(user));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while creating user {email}", request.Email);
            throw;
        }
    }
}
