using System;

using TemplateNetCore.Domain.Enums.Transactions;

namespace TemplateNetCore.Domain.Entities.Transactions
{
    public class Transaction : BaseEntity
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TargetKey { get; set; }
        public DateTime Date { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
