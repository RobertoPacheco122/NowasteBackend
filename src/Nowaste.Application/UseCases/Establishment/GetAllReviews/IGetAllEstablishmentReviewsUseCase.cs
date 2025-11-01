using Nowaste.Communication.Responses.Review;

namespace Nowaste.Application.UseCases.Establishment.GetAllReviews;

public interface IGetAllEstablishmentReviewsUseCase
{
    Task<ICollection<ResponseGetReviewByIdJson>> Execute(Guid establishmentId);
}
