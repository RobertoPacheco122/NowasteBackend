using Nowaste.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class PersonBuilder {
    public static PersonEntity Build() {
               var person = new Bogus.Faker<PersonEntity>()
            .RuleFor(p => p.Id, faker => faker.Random.Guid())
            .RuleFor(p => p.FullName, faker => faker.Name.FullName())
            .RuleFor(p => p.Cpf, faker => faker.Random.ReplaceNumbers("###########"))
            .RuleFor(p => p.PhoneNumber, faker => faker.Phone.PhoneNumber("###########"))
            .RuleFor(p => p.BirthDate, faker =>
                DateOnly.FromDateTime(
                    faker.Date.Between(
                        DateTime.UtcNow.AddYears(-80),
                        DateTime.UtcNow.AddYears(-18)
                    )
                )
            )
            .RuleFor(p => p.CreatedAt, faker => faker.Date.Past(1))
            .RuleFor(p => p.UpdatedAt, faker => faker.Date.Recent());

        return person.Generate();
    }
}
