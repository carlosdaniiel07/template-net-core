using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.UseCases.v1.Auth.SignUp;

namespace TemplateNetCore.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/sign-up")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpUseCase _signUpUseCase;

        public SignUpController(ISignUpUseCase signUpUseCase)
        {
            _signUpUseCase = signUpUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SignUpResponse), 200)]
        public async Task<IActionResult> Login([FromBody] SignUpRequest signInRequest) =>
            Ok(await _signUpUseCase.ExecuteAsync(signInRequest));
    }
}
