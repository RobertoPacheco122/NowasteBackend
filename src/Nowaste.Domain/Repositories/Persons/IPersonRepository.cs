using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Persons;

public interface IPersonRepository {
    public Task Add(PersonEntity persons);
}
