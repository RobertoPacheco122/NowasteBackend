using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Establishment;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Establishment;

internal class EstablishmentRepository(NowasteDbContext dbContext) : IEstablishmentRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(EstablishmentEntity establishment) {
        await _dbContext
            .Establishments
            .AddAsync(establishment);
    }

    public async Task<bool> ExistActiveEstablishmentWithCnpjOrEmail(string email, string cnpj) {
        return await _dbContext.Establishments
            .AsNoTracking()
            .AnyAsync(establishment =>
                establishment.Email.Equals(email) ||
                establishment.Cnpj.Equals(cnpj)
            );
    }
}
