using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.User;

public interface IUserUpdateOnlyRepository {
    Task<UserEntity> GetById(Guid Id);
    void Update(UserEntity user);
}
