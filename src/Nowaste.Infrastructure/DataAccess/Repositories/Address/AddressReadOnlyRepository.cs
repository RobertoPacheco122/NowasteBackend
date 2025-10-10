using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Address;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Address;

public class AddressReadOnlyRepository(NowasteDbContext dbContext) : IAddressReadOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<bool> ExistActiveWithId(Guid id) {
        return await _dbContext.Addresses
            .AsNoTracking()
            .AnyAsync(address => address.Id.Equals(id));
    }

    public async Task<ICollection<AddressEntity>> GetAllByEstablishment(Guid id) {
        return await _dbContext.Addresses
            .AsNoTracking()
            .Where(address => address.EstablishmentId == id)
            .ToListAsync();
    }

    public async Task<ICollection<AddressEntity>> GetAllByInstitution(Guid id) {
        return await _dbContext.Addresses
            .AsNoTracking()
            .Where(address => address.InstitutionId == id)
            .ToListAsync();
    }

    public async Task<ICollection<AddressEntity>> GetAllByPerson(Guid id) {
        return await _dbContext.Addresses
            .AsNoTracking()
            .Where(address => address.PersonId == id)
            .ToListAsync();
    }

    public async Task<AddressEntity?> GetById(Guid id) {
        return await _dbContext.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(address => address.Id == id);
    }
}
