using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nowaste.Application.UseCases.Review.Register;
using Nowaste.Application.UseCases.Review.RegisterEstablishmentResponse;
using Nowaste.Communication.Requests.Review;
using Nowaste.Communication.Responses;
using Nowaste.Communication.Responses.Review;
using Nowaste.Domain.Enums;

namespace Nowaste.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReviewController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredReviewJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterReviewUseCase useCase,
        [FromBody] RequestRegisterReviewJson request
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpPut("establishmnet-response/{reviewId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = Roles.ESTABLISHMENT_ADMIN)]
    public async Task<IActionResult> RegisterEstablishmentResponse(
        [FromServices] IRegisterEstablishmentReviewResponseUseCase useCase,
        [FromBody] RequestRegisterEstablishmentReviewResponseJson request,
        [FromRoute] Guid reviewId
    )
    {
        await useCase.Execute(reviewId, request);

        return NoContent();
    }
}
