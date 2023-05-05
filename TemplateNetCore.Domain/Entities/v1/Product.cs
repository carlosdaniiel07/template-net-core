namespace TemplateNetCore.Domain.Entities.v1
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Value { get; set; }
    }
}
