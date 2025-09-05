using Bogus;
using Nowaste.Communication.Requests.Users;
using Nowaste.Domain.Enums;

namespace CommonTestUtilities.Requests.Users;

public class RequestRegisterUserJsonBuilder {
    public static RequestRegisterUserJson Build() {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(r => r.Email, faker => faker.Internet.Email())
            .RuleFor(r => r.Cpf, _ => "79745307408")
            .RuleFor(r => r.FullName, faker => faker.Name.FullName())
            .RuleFor(r => r.Role, _ => Roles.CUSTOMER)
            .RuleFor(r => r.BirthDate, faker => DateOnly.FromDateTime(faker.Date.Past(30, DateTime.Now.AddYears(-18))))
            .RuleFor(r => r.PhoneNumber, faker => faker.Phone.PhoneNumber("##########"))
            .RuleFor(r => r.Password, faker => faker.Internet.Password(prefix: "!Aa1"))
            .RuleFor(r => r.Nickname, faker => faker.Internet.UserName());
    }
}
