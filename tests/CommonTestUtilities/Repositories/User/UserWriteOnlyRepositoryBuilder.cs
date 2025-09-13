using Moq;
using Nowaste.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories.User;

public class UserWriteOnlyRepositoryBuilder {
    private readonly Mock<IUserWriteOnlyRepository> _userWriteOnlyRepository;

    public UserWriteOnlyRepositoryBuilder() {
        _userWriteOnlyRepository = new Mock<IUserWriteOnlyRepository>();
    }

    public IUserWriteOnlyRepository Build() => _userWriteOnlyRepository.Object;

}
