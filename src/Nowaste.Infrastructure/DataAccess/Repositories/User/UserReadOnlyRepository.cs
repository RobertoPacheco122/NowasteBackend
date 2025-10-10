using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.User;

namespace Nowaste.Infrastructure.DataAccess.Repositories.User;

internal class UserReadOnlyRepository(NowasteDbContext dbContext) : IUserReadOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<bool> ExistActiveUserWithEmail(string email) {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email.Equals(email) && !user.IsDeleted);
    }

    public async Task<UserEntity?> GetById(Guid id) {
        return await _dbContext.Users
            .Include(user => user.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<UserEntity?> GetUserByEmail(string email) {
        return await _dbContext.Users
            .Include(user => user.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email) && !user.IsDeleted);
    }
}
