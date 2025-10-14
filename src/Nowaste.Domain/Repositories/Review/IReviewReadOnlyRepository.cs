using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Review;

public interface IReviewReadOnlyRepository
{
    Task<ReviewEntity?> GetById(Guid id);
    Task<bool> ExistActiveWithId(Guid id);
    Task<bool> ExistActiveForOrderId(Guid id);
}
