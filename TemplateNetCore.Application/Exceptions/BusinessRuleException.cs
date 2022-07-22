namespace TemplateNetCore.Application.Exceptions
{
    public class BusinessRuleException : CustomException
    {
        public BusinessRuleException(string message) : base(message, 400)
        {
            
        }
    }
}
