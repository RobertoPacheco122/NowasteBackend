using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Establishment;

public interface IEstablishmentWriteOnlyRepository
{
    Task Add(EstablishmentEntity establishment);
    Task AddOperatingDay(OperatingDayEntity operatingDay);
}
