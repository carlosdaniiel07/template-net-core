using System;

namespace TemplateNetCore.Service.Exceptions
{
    public abstract class CustomException : Exception
    {
        public int StatusCode { get; }

        public CustomException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
