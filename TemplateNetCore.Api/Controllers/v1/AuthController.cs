using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.UseCases.v1.Auth.SignIn;

namespace TemplateNetCore.Api.Controllers.v1
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISignInUseCase _signInUseCase;

        public AuthController(ISignInUseCase signInUseCase)
        {
            _signInUseCase = signInUseCase;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(SignInResponse), 200)]
        public async Task<IActionResult> Login([FromBody] SignInRequest signInRequest) =>
            Ok(await _signInUseCase.ExecuteAsync(signInRequest));
    }
}
