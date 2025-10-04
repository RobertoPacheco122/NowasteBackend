using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Address;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Address;

internal class AddressRepository(NowasteDbContext dbContext) : IAddressWriteOnlyRepository, IAddressReadOnlyRepository, IAddressUpdateOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(AddressEntity address) {
        await _dbContext.Addresses.AddAsync(address);
    }

    async Task<ICollection<AddressEntity>> IAddressReadOnlyRepository.GetAllByEstablishment(Guid id) {
        return await _dbContext.Addresses
            .AsNoTracking()
            .Where(address => address.EstablishmentId == id)
            .ToListAsync();
    }

    async Task<ICollection<AddressEntity>> IAddressReadOnlyRepository.GetAllByInstitution(Guid id) {
        return await _dbContext.Addresses
            .AsNoTracking()
            .Where(address => address.InstitutionId == id)
            .ToListAsync();
    }

    async Task<ICollection<AddressEntity>> IAddressReadOnlyRepository.GetAllByPerson(Guid id) {
        return await _dbContext.Addresses
            .AsNoTracking()
            .Where(address => address.PersonId == id)
            .ToListAsync();
    }
    async Task<ICollection<AddressEntity>> IAddressUpdateOnlyRepository.GetAllByEstablishment(Guid id) {
        return await _dbContext.Addresses
            .Where(address => address.EstablishmentId == id)
            .ToListAsync();
    }

    async Task<ICollection<AddressEntity>> IAddressUpdateOnlyRepository.GetAllByInstitution(Guid id) {
        return await _dbContext.Addresses
            .Where(address => address.InstitutionId == id)
            .ToListAsync();
    }

    async Task<ICollection<AddressEntity>> IAddressUpdateOnlyRepository.GetAllByPerson(Guid id) {
        return await _dbContext.Addresses
            .Where(address => address.PersonId == id)
            .ToListAsync();
    }

    public async Task<AddressEntity> GetById(Guid id) {
        return await _dbContext.Addresses
            .FirstAsync(address => address.Id == id);
    }

    public void Update(AddressEntity address) {
        _dbContext.Addresses.Update(address);
    }
}
