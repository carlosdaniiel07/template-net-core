﻿using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Entities.Users;

namespace TemplateNetCore.Domain.Interfaces.Users
{
    public interface IUserService
    {
        Task<User> GetById(Guid id);
        Guid GetLoggedUserId();
        Task<GetLoginResponseDto> Login(PostLoginDto postLoginDto);
        Task<User> SignUp(PostSignUpDto postSignUpDto);
    }
}
