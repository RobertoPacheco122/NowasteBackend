using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Review;

public interface IReviewWriteOnlyRepository
{
    Task Add(ReviewEntity review);
}
