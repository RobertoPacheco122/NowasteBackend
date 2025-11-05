using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<bool> ExistActiveUserWithCpf(string cpf);
    Task<UserEntity?> GetById(Guid id);
    Task<UserEntity?> GetUserByEmail(string email);
}
