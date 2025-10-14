using Nowaste.Communication.Requests.Review;

namespace Nowaste.Application.UseCases.Review.RegisterEstablishmentResponse;

public interface IRegisterEstablishmentReviewResponseUseCase
{
    Task Execute(Guid reviewId, RequestRegisterEstablishmentReviewResponseJson request);
}
