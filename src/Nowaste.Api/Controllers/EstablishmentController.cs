using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Establishment.GetAllAvailableForAddress;
using Nowaste.Application.UseCases.Establishment.GetById;
using Nowaste.Application.UseCases.Establishment.Register;
using Nowaste.Application.UseCases.Establishment.RegisterOperatingDay;
using Nowaste.Application.UseCases.Establishment.Update;
using Nowaste.Application.UseCases.Establishment.UpdateOperatingDay;
using Nowaste.Application.UseCases.Establishment.VinculateEmployee;
using Nowaste.Communication.Requests.Establishment;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Establishment;
using Nowaste.Domain.Enums;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstablishmentController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(
        typeof(ResponseRegisteredEstablishmentJson),
        StatusCodes.Status201Created
    )]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterEstablishmentUseCase useCase,
        [FromBody] RequestRegisterEstablishmentJson request
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpPost("vinculate-employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> VinculateEmployee(
        [FromServices] IVinculateEmployeeToEstablishmentUseCase useCase,
        [FromBody] RequestVinculateEmployeeToEstablishmentJson request
    )
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpPost("operating-day")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> RegisterOperatingDay(
        [FromServices] IRegisterOperatingDayUseCase useCase,
        [FromBody] RequestRegisterOperatingDayJson request
    )
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseGetEstablishmentByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetEstablishmentByIdUseCase useCase,
        [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("get-all-available-for-address/{id}")]
    [ProducesResponseType(
        typeof(ICollection<ResponseGetAllAvailableEstablishmentForAddressJson>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetAllAvailableForAddress(
        [FromServices] IGetAvailableEstablishmentForAddressUseCase useCase,
        [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateEstablishmentUseCase useCase,
        [FromBody] RequestUpdateEstablishmentJson request,
        [FromRoute] Guid id
    )
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPut("operating-day/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> UpdateOperatingDay(
        [FromServices] IUpdateOperatingDayUseCase useCase,
        [FromBody] RequestUpdateOperatingDayJson request,
        [FromRoute] Guid id
    )
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
