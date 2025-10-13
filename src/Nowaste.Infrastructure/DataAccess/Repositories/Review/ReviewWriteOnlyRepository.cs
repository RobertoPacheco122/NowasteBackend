using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Review;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Review;

internal class ReviewWriteOnlyRepository(NowasteDbContext dbContext) : IReviewWriteOnlyRepository
{
    public async Task Add(ReviewEntity review)
    {
        await dbContext.Reviews.AddAsync(review);
    }
}
