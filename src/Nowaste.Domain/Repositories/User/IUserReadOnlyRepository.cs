using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.User;
public interface IUserReadOnlyRepository {
    Task<UserEntity> GetById(Guid id);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<UserEntity?> GetUserByEmail(string email);
}
