using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Repositories.Person;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Person;

internal class PersonReadOnlyRepository(NowasteDbContext dbContext) : IPersonReadOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<bool> ExistActiveUserWithPhoneNumber(string phoneNumber)
    {
        return await _dbContext
            .Persons.AsNoTracking()
            .AnyAsync(person => person.PhoneNumber.Equals(phoneNumber) && !person.IsDeleted);
    }
}
