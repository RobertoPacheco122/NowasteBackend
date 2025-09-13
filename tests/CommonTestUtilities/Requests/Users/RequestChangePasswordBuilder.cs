using Bogus;
using Nowaste.Communication.Requests.Users;
using System.Net.NetworkInformation;

namespace CommonTestUtilities.Requests.Users;
public class RequestChangePasswordBuilder {

    public static RequestChangePasswordJson Build() {
        return new Faker<RequestChangePasswordJson>()
            .RuleFor(user => user.OldPassword, faker => faker.Internet.Password())
            .RuleFor(user => user.NewPassword, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}
