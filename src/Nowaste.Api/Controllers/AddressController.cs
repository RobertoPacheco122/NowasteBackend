using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Address.Register;
using Nowaste.Communication.Requests.Address;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Address;

namespace Nowaste.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase {
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredAddressJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterAddressUseCase useCase,
        [FromBody] RequestRegisterAddressJson request
    ) {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
