using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Application.UseCases.Address.GetAllByPerson;

public interface IGetAllAddressesByPersonUseCase
{
    Task<ICollection<ResponseGetAllAddressesJson>> Execute(Guid personId);
}
