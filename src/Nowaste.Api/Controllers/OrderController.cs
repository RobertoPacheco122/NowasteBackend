using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Order.GetById;
using Nowaste.Application.UseCases.Order.Register;
using Nowaste.Communication.Requests.Order;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Order;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredOrderJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterOrderUseCase useCase,
        [FromBody] RequestRegisterOrderJson request
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseGetOrderByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetOrderByIdUseCase useCase,
        [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }
}
