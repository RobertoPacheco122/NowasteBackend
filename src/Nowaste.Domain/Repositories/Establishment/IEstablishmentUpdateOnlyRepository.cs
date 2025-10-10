using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Establishment;

public interface IEstablishmentUpdateOnlyRepository {
    Task<EstablishmentEntity?> GetById(Guid Id);
    Task<OperatingDayEntity?> GetOperatingDayById(Guid operatingDayId);
    Task<OperatingDayEntity?> GetOperatingDayByDayOfWeek(Guid establishmentId, DayOfWeek dayOfWeek);
    void Update(EstablishmentEntity establishment);
    void UpdateOperatingDay(OperatingDayEntity operatingDay);
}
