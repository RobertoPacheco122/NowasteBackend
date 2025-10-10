using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Establishment;

public interface IEstablishmentReadOnlyRepository {
    Task<EstablishmentEntity?> GetById(Guid Id);
    Task<ICollection<EstablishmentEntity>> GetAllAvailableForAddress(Guid addressId);
    Task<OperatingDayEntity?> GetOperatingDayByDayOfWeek(Guid establishmentId, DayOfWeek dayOfWeek);
    Task<OperatingDayEntity?> GetOperatingDayById(Guid operatingDayId);
    Task<bool> ExistActiveWithId(Guid Id);
    Task<bool> ExistActiveEstablishmentWithCnpjOrEmail(string email, string cnpj);
}
