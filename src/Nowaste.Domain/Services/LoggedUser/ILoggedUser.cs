using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Services.LoggedUser;

public interface ILoggedUser {
    Task<UserEntity> Get();
}
