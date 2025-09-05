using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Persons;

namespace Nowaste.Infrastructure.DataAccess.Repositories;

internal class PersonsRepository(NowasteDbContext dbContext) : IPersonRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(PersonEntity persons) {
        await _dbContext.Persons.AddAsync(persons);
    }
}