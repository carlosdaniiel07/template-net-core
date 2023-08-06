using AutoMapper;
using TemplateNetCore.Domain.Commands.v1.Auth.SignUp;
using TemplateNetCore.Domain.Entities.v1;

namespace TemplateNetCore.Application.Commands.v1.Auth.SignUp
{
    public class SignUpCommandHandlerProfile : Profile
    {
        public SignUpCommandHandlerProfile()
        {
            CreateMap<SignUpCommand, User>();
            CreateMap<User, SignUpCommandResponse>();
        }
    }
}
