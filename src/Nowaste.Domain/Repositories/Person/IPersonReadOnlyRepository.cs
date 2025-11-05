namespace Nowaste.Domain.Repositories.Person;

public interface IPersonReadOnlyRepository
{
    Task<bool> ExistActiveUserWithPhoneNumber(string phoneNumber);
}
