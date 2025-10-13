using Nowaste.Communication.Requests.Establishment;
using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Application.UseCases.Establishment.Register;

public interface IRegisterEstablishmentUseCase
{
    Task<ResponseRegisteredEstablishmentJson> Execute(RequestRegisterEstablishmentJson request);
}
