using TemplateNetCore.Domain.Enums.Transactions;

namespace TemplateNetCore.Domain.Commands.CreateTransaction
{
    public class CreateTransactionCommandResponse
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public TransactionStatus Status { get; set; }
    }
}