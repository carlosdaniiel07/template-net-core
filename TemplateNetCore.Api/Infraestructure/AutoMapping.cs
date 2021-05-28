using AutoMapper;
using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Api.Infraestructure
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PostSignUpDto, User>();
        }
    }
}
