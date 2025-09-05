using Moq;
using Nowaste.Domain.Repositories.Persons;

namespace CommonTestUtilities.Repositories;
public class PersonRepositoryBuilder {
    private readonly Mock<IPersonRepository> _personRepository;

    public PersonRepositoryBuilder() {
        _personRepository = new Mock<IPersonRepository>();
    }
 
    public IPersonRepository Build() => _personRepository.Object;
}
