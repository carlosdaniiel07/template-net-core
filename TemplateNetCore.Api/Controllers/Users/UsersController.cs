using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Entities.Users;
using TemplateNetCore.Domain.Interfaces.Users;

namespace TemplateNetCore.Api.Controllers.Users
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public ActionResult SignUp([FromBody] PostSignUpDto signUpDto)
        {
            _userService.SignUp(signUpDto);
            return Ok();
        }

        [HttpPost("auth")]
        public ActionResult<GetLoginResponseDto> Login([FromBody] PostLoginDto postLoginDto)
        {
            return Ok(_userService.Login(postLoginDto));
        }
    }
}
