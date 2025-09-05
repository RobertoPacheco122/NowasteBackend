using Bogus;
using CommonTestUtilities.Cryptography;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;

namespace CommonTestUtilities.Entities;

public class UserBuilder {
    public static UserEntity Build() {
        var passwordEncrypter = new PasswordEncrypterBuild().Build();

        var user = new Faker<UserEntity>()
            .RuleFor(u => u.Id, faker => faker.Random.Guid())
            .RuleFor(u => u.Email, faker => faker.Internet.Email())
            .RuleFor(u => u.PasswordHash, (_, user) => passwordEncrypter.Encrypt(user.PasswordHash))
            .RuleFor(u => u.UserStatus, EUserStatus.Active)
            .RuleFor(u => u.Person, _ => PersonBuilder.Build())
            .RuleFor(u => u.Role, _ => Roles.CUSTOMER);

        return user.Generate();
    }

}
