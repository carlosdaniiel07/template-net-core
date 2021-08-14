using AutoMapper;
using TemplateNetCore.Domain.Dto.Transactions;
using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Infra.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PostSignUpDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<PostTransactionDto, Transaction>();
            CreateMap<Transaction, GetTransactionDto>();
        }
    }
}
