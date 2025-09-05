using Nowaste.Communication.Requests.Persons;
using Nowaste.Communication.Responses.Persons;

namespace Nowaste.Application.UseCases.Persons.Register;

public interface IRegisterPersonUseCase {
    Task<ResponseRegisteredPersonJson> Execute(RequestRegisterPersonJson request);
}
