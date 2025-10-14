using Nowaste.Communication.Requests.Address;

namespace Nowaste.Application.UseCases.Address.UpdateByEstablishment;

public interface IUpdateAddressByEstablishmentUseCase
{
    Task Execute(Guid addressId, RequestRegisterAddressJson request);
}
