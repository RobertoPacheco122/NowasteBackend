using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Product.GetAllByEstablishment;
using Nowaste.Application.UseCases.Product.GetAllCategories;
using Nowaste.Application.UseCases.Product.GetById;
using Nowaste.Application.UseCases.Product.Register;
using Nowaste.Application.UseCases.Product.RegisterCategory;
using Nowaste.Application.UseCases.Product.ToggleIsActive;
using Nowaste.Application.UseCases.Product.Update;
using Nowaste.Application.UseCases.Product.UpdatePrice;
using Nowaste.Communication.Requests.Product;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Product;
using Nowaste.Domain.Enums;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredProductJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterProductUseCase useCase,
        [FromBody] RequestRegisterProductJson request
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpPost("category")]
    [ProducesResponseType(typeof(ResponseRegisteredProductJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = Roles.NOWASTE_ADMIN)]
    public async Task<IActionResult> RegisterProductCategory(
        [FromServices] IRegisterProductCategoryUseCase useCase,
        [FromBody] RequestRegisterProductCategoryJson request
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseGetProductByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(
        [FromServices] IGetProductByIdUseCase useCase,
        Guid id
    )
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("category")]
    [ProducesResponseType(
        typeof(ICollection<ResponseGetAllProductCategoriesJson>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProductCategories(
        [FromServices] IGetAllProductCategoriesUseCase useCase
    )
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet("get-all-by-establishment/{id}")]
    [ProducesResponseType(
        typeof(ICollection<ResponseGetAllProductsByEstablishmentJson>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProductsByEstablishment(
        [FromServices] IGetAllProductsByEstablishmentUseCase useCase,
        Guid id
    )
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut("toggle-is-active/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> ToggleIsActive(
        [FromServices] IToggleIsProductActiveUseCase useCase,
        Guid id
    )
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateProductUseCase useCase,
        [FromBody] RequestUpdateProductJson request,
        Guid id
    )
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPut("price-{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> UpdatePrice(
        [FromServices] IUpdateProductPriceUseCase useCase,
        [FromBody] RequestUpdateProductPriceJson request,
        Guid id
    )
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
