using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Address;

public interface IAddressReadOnlyRepository
{
    Task<bool> ExistActiveWithId(Guid id);
    Task<AddressEntity?> GetById(Guid id);
    Task<ICollection<AddressEntity>> GetAllByEstablishment(Guid id);
    Task<ICollection<AddressEntity>> GetAllByInstitution(Guid id);
    Task<ICollection<AddressEntity>> GetAllByPerson(Guid id);
}
