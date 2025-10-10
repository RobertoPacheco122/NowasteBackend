using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.User;

namespace Nowaste.Infrastructure.DataAccess.Repositories.User;

internal class UserWriteOnlyRepository(NowasteDbContext dbContext) : IUserWriteOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(UserEntity user) {
        await _dbContext.Users.AddAsync(user);
    }
}
