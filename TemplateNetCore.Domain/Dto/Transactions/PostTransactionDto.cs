using System;

namespace TemplateNetCore.Domain.Dto.Transactions
{
    public class PostTransactionDto
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TargetKey { get; set; }
        public DateTime Date { get; set; }
    }
}
