using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Users;
public interface IUserRepository {
    public Task Add(UserEntity user);
    public Task<bool> ExistActiveUserWithEmail(string email);
    public Task<UserEntity?> GetUserByEmail(string email);
}