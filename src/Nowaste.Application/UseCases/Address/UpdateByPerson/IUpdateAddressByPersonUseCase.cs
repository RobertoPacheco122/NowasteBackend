using Nowaste.Communication.Requests.Address;

namespace Nowaste.Application.UseCases.Address.UpdateByPerson;

public interface IUpdateAddressByPersonUseCase
{
    Task Execute(Guid addressId, RequestRegisterAddressJson request);
}
