namespace TemplateNetCore.Domain.UseCases
{
    public interface IBaseUseCase<TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}
