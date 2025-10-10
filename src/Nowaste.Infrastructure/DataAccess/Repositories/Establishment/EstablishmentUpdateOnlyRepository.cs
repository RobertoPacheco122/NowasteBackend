using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Establishment;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Establishment;

internal class EstablishmentUpdateOnlyRepository(NowasteDbContext dbContext) : IEstablishmentUpdateOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<EstablishmentEntity?> GetById(Guid Id) {
        return await _dbContext.Establishments
           .FirstOrDefaultAsync(establishment => establishment.Id == Id);
    }

    public async Task<OperatingDayEntity?> GetOperatingDayByDayOfWeek(Guid establishmentId, DayOfWeek dayOfWeek) {
        return await _dbContext.OperatingDays
            .FirstOrDefaultAsync(operatingDay =>
                operatingDay.EstablishmentId.Equals(establishmentId) &&
                operatingDay.DayOfWeek.Equals(dayOfWeek)
            );
    }

    public async Task<OperatingDayEntity?> GetOperatingDayById(Guid operatingDayId) {
        return await _dbContext.OperatingDays
            .FirstOrDefaultAsync(operatingDay =>
                operatingDay.Id.Equals(operatingDayId)
            );
    }

    public void Update(EstablishmentEntity establishment) {
        _dbContext.Establishments.Update(establishment);
    }

    public void UpdateOperatingDay(OperatingDayEntity operatingDay) {
        _dbContext.OperatingDays.Update(operatingDay);
    }
}
