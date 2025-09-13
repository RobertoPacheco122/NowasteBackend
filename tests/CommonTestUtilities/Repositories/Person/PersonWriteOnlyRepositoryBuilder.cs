using Moq;
using Nowaste.Domain.Repositories.Person;

namespace CommonTestUtilities.Repositories.Person;

public class PersonWriteOnlyRepositoryBuilder {
    private readonly Mock<IPersonWriteOnlyRepository> _personWriteOnlyRepository;

    public PersonWriteOnlyRepositoryBuilder() {
        _personWriteOnlyRepository = new Mock<IPersonWriteOnlyRepository>();
    }

    public IPersonWriteOnlyRepository Build() => _personWriteOnlyRepository.Object;
}
