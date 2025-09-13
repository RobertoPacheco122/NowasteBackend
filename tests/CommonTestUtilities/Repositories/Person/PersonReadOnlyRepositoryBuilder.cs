using Moq;
using Nowaste.Domain.Repositories.Person;

namespace CommonTestUtilities.Repositories.Person;

public class PersonReadOnlyRepositoryBuilder {
    private readonly Mock<IPersonReadOnlyRepository> _personReadOnlyRepository;

    public void ExistActiveUserWithPhoneNumber(string phoneNumber) {
        _personReadOnlyRepository.Setup(personReadOnlyRepository => personReadOnlyRepository.ExistActiveUserWithPhoneNumber(phoneNumber))
            .ReturnsAsync(true);
    }

    public PersonReadOnlyRepositoryBuilder() {
        _personReadOnlyRepository = new Mock<IPersonReadOnlyRepository>();
    }

    public IPersonReadOnlyRepository Build() => _personReadOnlyRepository.Object;
}
