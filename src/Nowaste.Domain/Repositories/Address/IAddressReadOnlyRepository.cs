using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Address;

public interface IAddressReadOnlyRepository {
    Task<ICollection<AddressEntity>> GetAllByEstablishment(Guid id);
    Task<ICollection<AddressEntity>> GetAllByInstitution(Guid id);
    Task<ICollection<AddressEntity>> GetAllByPerson(Guid id);
}
