using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.User;

namespace Nowaste.Infrastructure.DataAccess.Repositories.User;

internal class UserRepository(NowasteDbContext dbContext) : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository {
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

    async Task<UserEntity> IUserReadOnlyRepository.GetById(Guid id) {
        return await _dbContext.Users
            .Include(user => user.Person)
            .AsNoTracking()
            .FirstAsync(user => user.Id == id);
    }

    async Task<UserEntity> IUserUpdateOnlyRepository.GetById(Guid id) {
        return await _dbContext.Users
            .Include(user => user.Person)
            .FirstAsync(user => user.Id == id);
    }

    public void Update(UserEntity user) {
        _dbContext.Users.Update(user);
    }
}