using Nowaste.Communication.Requests.Address;
using Nowaste.Communication.Responses.Address;

namespace Nowaste.Application.UseCases.Address.Register;

public interface IRegisterAddressUseCase
{
    Task<ResponseRegisteredAddressJson> Execute(RequestRegisterAddressJson request);
}
