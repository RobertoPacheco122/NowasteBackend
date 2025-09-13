using Moq;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories.User;

public class UserUpdateOnlyRepository {
   public static IUserUpdateOnlyRepository Build(UserEntity user) {
        var mock = new Mock<IUserUpdateOnlyRepository>();

        mock.Setup(userUpdateOnlyRepository => userUpdateOnlyRepository.GetById(user.Id)).ReturnsAsync(user);

        return mock.Object;
   }
}
