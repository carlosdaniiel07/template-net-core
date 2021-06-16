using System;
using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Enums.Transactions;

namespace TemplateNetCore.Domain.Dto.Transactions
{
    public class GetTransactionDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TargetKey { get; set; }
        public DateTime Date { get; set; }
        public TransactionStatus Status { get; set; }
        public GetUserDto User { get; set; }
    }
}
