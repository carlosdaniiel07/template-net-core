namespace TemplateNetCore.Domain.Models.v1
{
    public class Notification
    {
        public string Code { get; private set; }
        public string Message { get; private set; }

        public Notification(string code) : this(code, null)
        {
            
        }

        public Notification(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
