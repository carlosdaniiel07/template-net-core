namespace TemplateNetCore.Domain.Models.v1;

public class Result<T>
{
    public T Data { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    private Result(T data)
    {
        IsSuccess = true;
        Data = data;
    }

    public static Result<T> Success(T data = default) => new(data);
    public static Result<T> Failure(Error error) => new(error);
}
