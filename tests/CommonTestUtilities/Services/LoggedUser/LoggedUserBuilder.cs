using Moq;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Services.LoggedUser;

namespace CommonTestUtilities.Services.LoggedUser;
public class LoggedUserBuilder {
    public static ILoggedUser Build(UserEntity user) {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        return mock.Object;
    }
}
