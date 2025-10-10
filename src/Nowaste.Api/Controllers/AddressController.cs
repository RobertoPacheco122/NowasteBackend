using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Address.Delete;
using Nowaste.Application.UseCases.Address.GetAllByEstablishment;
using Nowaste.Application.UseCases.Address.GetAllByInstitution;
using Nowaste.Application.UseCases.Address.GetAllByPerson;
using Nowaste.Application.UseCases.Address.GetById;
using Nowaste.Application.UseCases.Address.Register;
using Nowaste.Application.UseCases.Address.UpdateByEstablishment;
using Nowaste.Application.UseCases.Address.UpdateByInstitution;
using Nowaste.Application.UseCases.Address.UpdateByPerson;
using Nowaste.Communication.Requests.Address;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Address;
using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AddressController : ControllerBase {
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredAddressJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterAddressUseCase useCase,
        [FromBody] RequestRegisterAddressJson request
    ) {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseGetAddressByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetAddressByIdUseCase useCase,
        [FromRoute] Guid id
    ) {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("get-all-by-establishment/{id}")]
    [ProducesResponseType(typeof(ICollection<ResponseGetAllAddressesJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByEstablishment(
        [FromServices] IGetAllAddressesByEstablishmentUseCase useCase,
        [FromRoute] Guid id
    ) {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("get-all-by-institution/{id}")]
    [ProducesResponseType(typeof(ICollection<ResponseGetAllAddressesJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByInstitution(
        [FromServices] IGetAllAddressesByInstitutionUseCase useCase,
        [FromRoute] Guid id
    ) {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("get-all-by-person/{id}")]
    [ProducesResponseType(typeof(ICollection<ResponseGetAllAddressesJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByPerson(
        [FromServices] IGetAllAddressesByPersonUseCase useCase,
        [FromRoute] Guid id
    ) {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut("update-by-establishment/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByEstablishment(
        [FromServices] IUpdateAddressByEstablishmentUseCase useCase,
        [FromBody] RequestRegisterAddressJson request,
        [FromRoute] Guid id
    ) {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPut("update-by-institution/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByInstitution(
        [FromServices] IUpdateAddressByInstitutionUseCase useCase,
        [FromBody] RequestRegisterAddressJson request,
        [FromRoute] Guid id
    ) {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPut("update-by-person/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByPerson(
        [FromServices] IUpdateAddressByPersonUseCase useCase,
        [FromBody] RequestRegisterAddressJson request,
        [FromRoute] Guid id
    ) {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteAddressUseCase useCase,
        [FromRoute] Guid id
    ) {
        await useCase.Execute(id);

        return NoContent();
    }
}
