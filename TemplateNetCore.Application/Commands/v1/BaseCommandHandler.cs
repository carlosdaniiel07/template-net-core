using MediatR;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Application.Commands.v1;

public abstract class BaseCommandHandler<THandler, TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : IRequest<Result<TResponse>>
{
    public abstract Task<Result<TResponse>> Handle(TCommand request, CancellationToken cancellationToken);

    protected Result<TResponse> Failure(Error error) =>
        Result<TResponse>.Failure(error);

    protected Result<TResponse> Success(TResponse response) =>
        Result<TResponse>.Success(response);
}
