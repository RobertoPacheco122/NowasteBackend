using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Establishment;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Establishment;

internal class EstablishmentWriteOnlyRepository(NowasteDbContext dbContext)
    : IEstablishmentWriteOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(EstablishmentEntity establishment)
    {
        await _dbContext.Establishments.AddAsync(establishment);
    }

    public async Task AddOperatingDay(OperatingDayEntity operatingDay)
    {
        await _dbContext.OperatingDays.AddAsync(operatingDay);
    }
}
