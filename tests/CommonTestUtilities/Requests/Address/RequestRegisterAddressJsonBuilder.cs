using Bogus;
using Nowaste.Communication.Requests.Address;

namespace CommonTestUtilities.Requests.Address;

public class RequestRegisterAddressJsonBuilder {
    public static RequestRegisterAddressJson Build() {
        return new Faker<RequestRegisterAddressJson>()
            .RuleFor(r => r.StreetName, faker => faker.Address.StreetName())
            .RuleFor(r => r.City, faker => faker.Address.City())
            .RuleFor(r => r.Number, faker => faker.Address.BuildingNumber())
            .RuleFor(r => r.State, faker => faker.Address.State())
            .RuleFor(r => r.ZipCode, faker => faker.Address.ZipCode(format: "########"))
            .RuleFor(r => r.PersonId, faker => faker.Random.Uuid());
    }
}
