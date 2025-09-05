using Nowaste.Domain.Repositories;

namespace Nowaste.Infrastructure.DataAccess;

internal class UnitOfWork(NowasteDbContext dbContext) : IUnitOfWork {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}