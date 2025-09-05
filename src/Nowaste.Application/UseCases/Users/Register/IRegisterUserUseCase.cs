using Nowaste.Communication.Requests.Users;
using Nowaste.Communication.Responses.Users;

namespace Nowaste.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase {
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}