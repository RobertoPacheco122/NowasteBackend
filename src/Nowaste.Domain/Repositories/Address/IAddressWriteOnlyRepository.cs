using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Address;

public interface IAddressWriteOnlyRepository {
    public Task Add(AddressEntity address);
}
