using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Users.Register;
using Nowaste.Communication.Requests.Users;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Users;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request
    ) {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
