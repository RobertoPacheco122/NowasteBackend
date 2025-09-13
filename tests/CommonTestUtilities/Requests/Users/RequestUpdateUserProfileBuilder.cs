using Bogus;
using Nowaste.Communication.Enums;
using Nowaste.Communication.Requests.Users;

namespace CommonTestUtilities.Requests.Users;
public class RequestUpdateUserProfileBuilder {
    public static RequestUpdateUserProfileJson Build() {
        return new Faker<RequestUpdateUserProfileJson>()
            .RuleFor(r => r.Nickname, faker => faker.Internet.UserName())
            .RuleFor(r => r.Role, _ => "customer")
            .RuleFor(r => r.PhoneNumber, faker => faker.Phone.PhoneNumber("###########"))
            .RuleFor(r => r.UserStatus, _ => EUserStatus.Active);
    }
}
