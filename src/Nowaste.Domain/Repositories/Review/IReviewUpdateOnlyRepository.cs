using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Review;

public interface IReviewUpdateOnlyRepository
{
    Task<ReviewEntity?> GetById(Guid id);
    void Update(ReviewEntity review);
}
