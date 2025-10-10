using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Person;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Person;

internal class PersonWriteOnlyRepository(NowasteDbContext dbContext) : IPersonWriteOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(PersonEntity person) {
        await _dbContext.Persons.AddAsync(person);
    }
}
