using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Users.ChangePassword;
using Nowaste.Application.UseCases.Users.Register;
using Nowaste.Application.UseCases.Users.UpdateProfile;
using Nowaste.Communication.Requests.Users;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Users;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [HttpPut]
    public async Task<IActionResult> UpdateProfile(
        [FromServices] IUpdateUserProfileUseCase useCase,
        [FromBody] RequestUpdateUserProfileJson request
    )
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpPut("change-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordJson request
    )
    {
        await useCase.Execute(request);

        return NoContent();
    }
}
