using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    Task Add(UserEntity user);
}
