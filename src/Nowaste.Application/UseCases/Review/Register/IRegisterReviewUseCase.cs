using Nowaste.Communication.Requests.Review;
using Nowaste.Communication.Responses.Review;

namespace Nowaste.Application.UseCases.Review.Register;

public interface IRegisterReviewUseCase
{
    Task<ResponseRegisteredReviewJson> Execute(RequestRegisterReviewJson request);
}
