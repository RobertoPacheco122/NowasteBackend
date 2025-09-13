using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Establishments.Register;
using Nowaste.Communication.Requests.Establishments;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Address;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstablishmentController : ControllerBase {
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredAddressJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterEstablishmentUseCase useCase,
        [FromBody] RequestRegisterEstablishmentJson request
    ) {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
