using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Security.Tokens;

public interface IAccessTokenGenerator {
    string Generate(UserEntity user);
}
