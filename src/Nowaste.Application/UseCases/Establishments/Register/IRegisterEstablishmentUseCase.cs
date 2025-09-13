using Nowaste.Communication.Requests.Establishments;
using Nowaste.Communication.Responses.Establishments;

namespace Nowaste.Application.UseCases.Establishments.Register;

public interface IRegisterEstablishmentUseCase {
    Task<ResponseRegisteredEstablishmentJson> Execute(RequestRegisterEstablishmentJson request);
}
