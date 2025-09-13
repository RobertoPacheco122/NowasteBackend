using Moq;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories.User;

public class UserReadOnlyRepositoryBuilder {
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepository;

    public UserReadOnlyRepositoryBuilder() {
        _userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistActiveUserWithEmail(string email) {
        _userReadOnlyRepository.Setup(userReadOnlyRepository => userReadOnlyRepository.ExistActiveUserWithEmail(email))
            .ReturnsAsync(true);
    }

    public UserReadOnlyRepositoryBuilder GetUserByEmail(UserEntity user) {
        _userReadOnlyRepository.Setup(userReadOnlyRepository => userReadOnlyRepository.GetUserByEmail(user.Email))
            .ReturnsAsync(user);

        return this;
    }

    public IUserReadOnlyRepository Build() => _userReadOnlyRepository.Object;

}
