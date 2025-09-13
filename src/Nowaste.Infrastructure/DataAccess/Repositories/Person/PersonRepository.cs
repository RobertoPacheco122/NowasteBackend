using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Person;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Person;

internal class PersonRepository(NowasteDbContext dbContext) : IPersonReadOnlyRepository, IPersonWriteOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(PersonEntity persons) {
        await _dbContext.Persons.AddAsync(persons);
    }

    public async Task<bool> ExistActiveUserWithPhoneNumber(string phoneNumber) {
        return await _dbContext.Persons
            .AsNoTracking()
            .AnyAsync(person => person.PhoneNumber.Equals(phoneNumber) && !person.IsDeleted);
    }
}