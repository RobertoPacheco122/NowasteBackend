using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Person;

public interface IPersonWriteOnlyRepository
{
    Task Add(PersonEntity person);
}
