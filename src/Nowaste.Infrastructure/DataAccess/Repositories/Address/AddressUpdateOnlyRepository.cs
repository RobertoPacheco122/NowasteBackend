using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Address;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Address;

public class AddressUpdateOnlyRepository(NowasteDbContext dbContext) : IAddressUpdateOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<ICollection<AddressEntity>> GetAllByEstablishment(Guid id) {
        return await _dbContext.Addresses
            .Where(address => address.EstablishmentId == id)
            .ToListAsync();
    }

    public async Task<ICollection<AddressEntity>> GetAllByInstitution(Guid id) {
        return await _dbContext.Addresses
            .Where(address => address.InstitutionId == id)
            .ToListAsync();
    }

    public async Task<ICollection<AddressEntity>> GetAllByPerson(Guid id) {
        return await _dbContext.Addresses
            .Where(address => address.PersonId == id)
            .ToListAsync();
    }

    public async Task<AddressEntity?> GetById(Guid id) {
        return await _dbContext.Addresses
            .FirstOrDefaultAsync(address => address.Id == id);
    }

    public void Update(AddressEntity address) {
        _dbContext.Addresses.Update(address);
    }
}
