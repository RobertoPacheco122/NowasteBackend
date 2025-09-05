using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Auth.Login;
using Nowaste.Communication.Requests.Auth;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Users;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {
    [HttpPost("Login")]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Authenticate(
        [FromServices] ILoginUseCase useCase,
        [FromBody] RequestLoginJson request
    ) {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}