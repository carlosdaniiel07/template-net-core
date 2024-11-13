namespace TemplateNetCore.Domain.Exceptions;

public abstract class BaseException : Exception
{
    public int StatusCode { get; private set; }

    protected BaseException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
