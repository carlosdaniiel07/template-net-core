namespace TemplateNetCore.Application.Exceptions
{
    public class ValidationException : CustomException
    {
        public ValidationException(string message) : base(message, 400)
        {

        }
    }
}