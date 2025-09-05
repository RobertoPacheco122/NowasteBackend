using Moq;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Users;

namespace CommonTestUtilities.Repositories;

public class UserRepositoryBuilder {
    private readonly Mock<IUserRepository> _userRepository;

    public UserRepositoryBuilder() {
        _userRepository = new Mock<IUserRepository>();
    }

    public void ExistActiveUserWithEmail(string email) {
        _userRepository.Setup(userRepository => userRepository.ExistActiveUserWithEmail(email))
            .ReturnsAsync(true);
    }

    public UserRepositoryBuilder GetUserByEmail(UserEntity user) {
        _userRepository.Setup(userRepository => userRepository.GetUserByEmail(user.Email))
            .ReturnsAsync(user);

        return this;
    }

    public IUserRepository Build() => _userRepository.Object;
}
