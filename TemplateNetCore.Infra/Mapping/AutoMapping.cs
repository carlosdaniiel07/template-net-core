using AutoMapper;
using TemplateNetCore.Domain.Commands.CreateTransaction;
using TemplateNetCore.Domain.Commands.SignUp;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Infra.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<SignUpCommand, User>();
            CreateMap<User, SignUpCommandResponse>();

            CreateMap<CreateTransactionCommand, Transaction>();
            CreateMap<Transaction, CreateTransactionCommandResponse>();
        }
    }
}
