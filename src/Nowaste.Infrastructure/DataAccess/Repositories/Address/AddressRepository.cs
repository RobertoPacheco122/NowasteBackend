using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Address;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Address;

internal class AddressRepository(NowasteDbContext dbContext) : IAddressWriteOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(AddressEntity address) {
        await _dbContext.AddAsync(address);
    }
}
