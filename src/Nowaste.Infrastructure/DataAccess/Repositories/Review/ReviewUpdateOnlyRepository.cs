using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Review;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Review;

internal class ReviewUpdateOnlyRepository(NowasteDbContext dbContext) : IReviewUpdateOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<ReviewEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Reviews.Include(review => review.Order)
            .FirstOrDefaultAsync(review => review.Id.Equals(id));
    }

    public void Update(ReviewEntity review)
    {
        _dbContext.Reviews.Update(review);
    }
}
