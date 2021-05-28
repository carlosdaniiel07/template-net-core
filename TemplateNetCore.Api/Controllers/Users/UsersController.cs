using AutoMapper;
using System.Threading.Tasks;
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
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            _service = userService;
            _mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] PostSignUpDto signUpDto)
        {
            await _service.SignUp(_mapper.Map<User>(signUpDto));
            return Ok();
        }

        [HttpPost("auth")]
        public async Task<ActionResult<GetLoginResponseDto>> Login([FromBody] PostLoginDto postLoginDto)
        {
            return Ok(await _service.Login(postLoginDto));
        }
    }
}
