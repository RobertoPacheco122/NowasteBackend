using Moq;
using Nowaste.Domain.Security.Cryptography;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncrypterBuild {
    private readonly Mock<IPasswordEncrypter> _passwordEncrypter;

    public PasswordEncrypterBuild() {
        _passwordEncrypter = new Mock<IPasswordEncrypter>();

        _passwordEncrypter.Setup(passwordEncrypter => passwordEncrypter.Encrypt(It.IsAny<string>()))
            .Returns("!%dlasdasd534");
    }

    public PasswordEncrypterBuild Verify(string? password) {
        if(string.IsNullOrWhiteSpace(password) is false)
            _passwordEncrypter.Setup(passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>()))
                .Returns(true);

        return this;
    }

    public IPasswordEncrypter Build() => _passwordEncrypter.Object;
}
