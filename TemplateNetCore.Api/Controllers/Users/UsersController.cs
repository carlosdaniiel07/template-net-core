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

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] PostSignUpDto postSignUpDto)
        {
            await _service.SignUp(postSignUpDto);
            return Ok();
        }

        [HttpPost("auth")]
        public async Task<ActionResult<GetLoginResponseDto>> Login([FromBody] PostLoginDto postLoginDto)
        {
            return Ok(await _service.Login(postLoginDto));
        }
    }
}
