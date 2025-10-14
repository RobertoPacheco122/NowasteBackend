using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Review;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Review;

internal class ReviewReadOnlyRepository(NowasteDbContext dbContext) : IReviewReadOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<bool> ExistActiveForOrderId(Guid id)
    {
        return await _dbContext.Reviews.AnyAsync(review => review.OrderId.Equals(id));
    }

    public async Task<bool> ExistActiveWithId(Guid id)
    {
        return await _dbContext.Reviews.AnyAsync(review => review.Id.Equals(id));
    }

    public async Task<ReviewEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Reviews.AsNoTracking()
            .Include(review => review.Person)
            .Include(review => review.Order)
            .ThenInclude(order => order.OrderItems)
            .Include(review => review.Order)
            .ThenInclude(order => order.Establishment)
            .FirstOrDefaultAsync(review => review.Id.Equals(id));
    }
}
