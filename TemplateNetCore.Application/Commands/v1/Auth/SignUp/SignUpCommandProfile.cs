using AutoMapper;
using TemplateNetCore.Domain.Commands.v1.Auth.SignUp;
using TemplateNetCore.Domain.Entities.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignUp
{
    public class SignUpCommandProfile : Profile
    {
        public SignUpCommandProfile()
        {
            CreateMap<SignUpCommand, User>();
            CreateMap<User, SignUpCommandResponse>();
        }
    }
}
