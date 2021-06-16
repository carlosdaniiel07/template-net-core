using System;
using TemplateNetCore.Domain.Entities.Users;
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
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
