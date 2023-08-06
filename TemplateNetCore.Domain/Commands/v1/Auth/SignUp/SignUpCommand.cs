﻿using MediatR;

namespace TemplateNetCore.Domain.Commands.v1.Auth.SignUp
{
    public class SignUpCommand : IRequest<SignUpCommandResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}