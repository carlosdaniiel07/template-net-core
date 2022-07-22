using MediatR;

namespace TemplateNetCore.Domain.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<CreateTransactionCommandResponse>
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}