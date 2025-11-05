using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories.Establishment;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Establishment;

internal class EstablishmentReadOnlyRepository(NowasteDbContext dbContext)
    : IEstablishmentReadOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<bool> ExistActiveEstablishmentWithCnpjOrEmail(string email, string cnpj)
    {
        return await _dbContext
            .Establishments.AsNoTracking()
            .AnyAsync(establishment =>
                establishment.Email.Equals(email) || establishment.Cnpj.Equals(cnpj)
            );
    }

    public async Task<bool> ExistActiveWithId(Guid Id)
    {
        return await _dbContext
            .Establishments.AsNoTracking()
            .AnyAsync(establishment => establishment.Id.Equals(Id));
    }

    private static double CalculateDistanceInMeters(
        double lon1,
        double lat1,
        double lon2,
        double lat2
    )
    {
        const double R = 6371000;

        var latRad1 = ToRadians(lat1);
        var latRad2 = ToRadians(lat2);
        var deltaLat = ToRadians(lat2 - lat1);
        var deltaLon = ToRadians(lon2 - lon1);

        var a =
            Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2)
            + Math.Cos(latRad1)
                * Math.Cos(latRad2)
                * Math.Sin(deltaLon / 2)
                * Math.Sin(deltaLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        var distance = R * c;

        return distance;
    }

    private static double ToRadians(double degrees) => degrees * (Math.PI / 180.0);

    public async Task<ICollection<EstablishmentEntity>> GetAllAvailableForAddress(Guid addressId)
    {
        var addressEntity = await _dbContext
            .Addresses.AsNoTracking()
            .FirstAsync(address => address.Id == addressId);

        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        var addressPoint = geometryFactory.CreatePoint(
            new Coordinate(addressEntity.Longitude, addressEntity.Latitude)
        );

        var establishments = await _dbContext
            .Establishments.AsNoTracking()
            .Include(establishment => establishment.Reviews)
            .Include(establishment => establishment.OperatingDays)
            .Include(establishment => establishment.Products)
            .ThenInclude(product => product.ProductCategory)
            .Include(establishment => establishment.Products)
            .ThenInclude(product => product.PriceHistories)
            .Include(establishment => establishment.Addresses)
            .Where(establishment =>
                establishment.Status == EEstablishmentStatus.Active
                && establishment.Addresses.Any(address =>
                    address.AddressType == EAddressType.Operational
                )
            )
            .ToListAsync();

        var withinServiceRadiusEstablishments = establishments
            .Where(establishment =>
                establishment.Addresses.Any(address =>
                {
                    var distanceInMeters = CalculateDistanceInMeters(
                        addressEntity.Longitude,
                        addressEntity.Latitude,
                        address.Longitude,
                        address.Latitude
                    );

                    return distanceInMeters <= establishment.ServiceRadiusInMeters;
                })
            )
            .ToList();

        return withinServiceRadiusEstablishments;
    }

    public async Task<EstablishmentEntity?> GetById(Guid Id)
    {
        return await _dbContext
            .Establishments.AsNoTracking()
            .Include(establishment => establishment.Reviews)
            .Include(establishment => establishment.OperatingDays)
            .Include(establishment => establishment.Addresses)
            .FirstOrDefaultAsync(establishment => establishment.Id == Id);
    }

    public async Task<OperatingDayEntity?> GetOperatingDayByDayOfWeek(
        Guid establishmentId,
        DayOfWeek dayOfWeek
    )
    {
        return await _dbContext
            .OperatingDays.AsNoTracking()
            .FirstOrDefaultAsync(operatingDay =>
                operatingDay.EstablishmentId.Equals(establishmentId)
                && operatingDay.DayOfWeek.Equals(dayOfWeek)
            );
    }

    public async Task<OperatingDayEntity?> GetOperatingDayById(Guid operatingDayId)
    {
        return await _dbContext
            .OperatingDays.AsNoTracking()
            .FirstOrDefaultAsync(operatingDay => operatingDay.Id.Equals(operatingDayId));
    }
}
