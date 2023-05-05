using Microsoft.AspNetCore.Mvc;
using TemplateNetCore.Domain.UseCases.v1.Auth.SignIn;

namespace TemplateNetCore.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/sign-in")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class SignInController : ControllerBase
    {
        private readonly ISignInUseCase _signInUseCase;

        public SignInController(ISignInUseCase signInUseCase)
        {
            _signInUseCase = signInUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SignInResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] SignInRequest signInRequest) =>
            Ok(await _signInUseCase.ExecuteAsync(signInRequest));
    }
}
