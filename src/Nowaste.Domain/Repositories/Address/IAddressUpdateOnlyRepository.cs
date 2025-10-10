using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Address;

public interface IAddressUpdateOnlyRepository {
    Task<AddressEntity?> GetById(Guid id);
    Task<ICollection<AddressEntity>> GetAllByEstablishment(Guid id);
    Task<ICollection<AddressEntity>> GetAllByInstitution(Guid id);
    Task<ICollection<AddressEntity>> GetAllByPerson(Guid id);
    void Update(AddressEntity address);
}
