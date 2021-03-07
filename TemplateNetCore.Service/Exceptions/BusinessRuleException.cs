namespace TemplateNetCore.Service.Exceptions
{
    public class BusinessRuleException : CustomException
    {
        public BusinessRuleException(string message) : base(message, 400)
        {
            
        }
    }
}
