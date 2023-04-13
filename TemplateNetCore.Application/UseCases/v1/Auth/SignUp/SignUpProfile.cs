using AutoMapper;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.UseCases.v1.Auth.SignUp;

namespace TemplateNetCore.Application.UseCases.v1.Auth.SignUp
{
    public class SignUpProfile : Profile
    {
        public SignUpProfile()
        {
            CreateMap<SignUpRequest, User>();
            CreateMap<User, SignUpResponse>();
        }
    }
}
