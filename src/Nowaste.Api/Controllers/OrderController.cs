using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Order.Checkout;
using Nowaste.Application.UseCases.Order.ConfirmPayment;
using Nowaste.Application.UseCases.Order.GetAllByEstablishment;
using Nowaste.Application.UseCases.Order.GetAllByPerson;
using Nowaste.Application.UseCases.Order.GetById;
using Nowaste.Application.UseCases.Order.GetByPaymentSessionId;
using Nowaste.Application.UseCases.Order.Register;
using Nowaste.Application.UseCases.Order.UpdateStatus;
using Nowaste.Communication.Requests.Order;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Order;
using Nowaste.Domain.Enums;
using Stripe;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredOrderJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterOrderUseCase useCase,
        [FromBody] RequestRegisterOrderJson request
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpPost("confirm-payment-webhook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmPaymentWebhook(
        [FromServices] IOrderConfirmPaymentUseCase useCase,
        [FromServices] IConfiguration configuration
    )
    {
        var webhookSecretKey = configuration.GetValue<string>("Settings:Stripe:WebhookSecretKey");

        var requestBodyAsJson = await new StreamReader(Request.Body).ReadToEndAsync();

        var signatureHeader = Request.Headers["Stripe-Signature"];

        var stripeEvent = EventUtility.ConstructEvent(
            requestBodyAsJson,
            signatureHeader,
            webhookSecretKey
        );

        await useCase.Execute(stripeEvent);

        return Ok();
    }

    [HttpPost("checkout")]
    [ProducesResponseType(typeof(ResponseOrderCheckoutJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> Checkout(
        [FromServices] IOrderCheckoutUseCase useCase,
        [FromBody] RequestOrderCheckoutJson request
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet("get-all-by-establishment")]
    [ProducesResponseType(
        typeof(ICollection<ResponseGetAllOrdersByEstablishmentJson>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetAllByEstablishment(
        [FromServices] IGetAllOrdersByEstablishmentUseCase useCase
    )
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet("get-all-by-person")]
    [ProducesResponseType(typeof(ICollection<ResponseGetOrderByIdJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetAllByPerson(
        [FromServices] IGetAllOrdersByPersonUseCase useCase
    )
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseGetOrderByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetById(
        [FromServices] IGetOrderByIdUseCase useCase,
        [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("payment-session/{id}")]
    [ProducesResponseType(typeof(ResponseGetOrderByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetByPaymentSessionId(
        [FromServices] IGetOrderByPaymentSessionIdUseCase useCase,
        [FromRoute] string id
    )
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut("update-status/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize(Roles = $"{Roles.ESTABLISHMENT_ADMIN},{Roles.ESTABLISHMENT_EMPLOYEE}")]
    public async Task<IActionResult> UpdateOrderStatus(
        [FromServices] IUpdateOrderStatusUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestUpdateOrderStatusJson request
    )
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
