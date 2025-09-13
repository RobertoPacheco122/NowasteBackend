using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Person;

public interface IPersonRepository {
    public Task Add(PersonEntity persons);
}
