using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.User;

namespace Nowaste.Infrastructure.DataAccess.Repositories.User;

internal class UserUpdateOnlyRepository(NowasteDbContext dbContext) : IUserUpdateOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<UserEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Users.Include(user => user.Person)
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public void Update(UserEntity user)
    {
        _dbContext.Users.Update(user);
    }
}
