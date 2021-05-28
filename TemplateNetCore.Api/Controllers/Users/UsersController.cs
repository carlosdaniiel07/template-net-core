using Microsoft.AspNetCore.Mvc;

using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Interfaces.Users;

namespace TemplateNetCore.Api.Controllers.Users
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService userService)
        {
            _service = userService;
        }

        [HttpPost("signup")]
        public ActionResult SignUp([FromBody] PostSignUpDto signUpDto)
        {
            _service.SignUp(signUpDto);
            return Ok();
        }

        [HttpPost("auth")]
        public ActionResult<GetLoginResponseDto> Login([FromBody] PostLoginDto postLoginDto)
        {
            return Ok(_service.Login(postLoginDto));
        }
    }
}
