using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Establishment;

public interface IEstablishmentRepository {
    public Task Add(EstablishmentEntity establishment);
    public Task<bool> ExistActiveEstablishmentWithCnpjOrEmail(string email, string cnpj);
}
