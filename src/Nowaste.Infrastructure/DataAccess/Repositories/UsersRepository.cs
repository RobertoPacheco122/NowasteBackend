using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Users;

namespace Nowaste.Infrastructure.DataAccess.Repositories;

internal class UsersRepository(NowasteDbContext dbContext) : IUserRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(UserEntity user) {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email) {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email.Equals(email) && !user.IsDeleted);
    }

    public async Task<UserEntity?> GetUserByEmail(string email) {
        return await _dbContext.Users
            .Include(user => user.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email) && !user.IsDeleted);
    }
}