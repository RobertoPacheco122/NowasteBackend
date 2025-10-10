using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.User;
public interface IUserReadOnlyRepository {
    Task<UserEntity?> GetById(Guid id);
    Task<UserEntity?> GetUserByEmail(string email);
    Task<bool> ExistActiveUserWithEmail(string email);
}
