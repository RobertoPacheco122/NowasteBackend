using Moq;
using Nowaste.Domain.Repositories.Address;

namespace CommonTestUtilities.Repositories.Address;

public class AddressWriteOnlyRepositoryBuilder {
    private readonly Mock<IAddressWriteOnlyRepository> _addressWriteOnlyRepository;

    public AddressWriteOnlyRepositoryBuilder() {
        _addressWriteOnlyRepository = new Mock<IAddressWriteOnlyRepository>();
    }

    public IAddressWriteOnlyRepository Build() => _addressWriteOnlyRepository.Object;
}
