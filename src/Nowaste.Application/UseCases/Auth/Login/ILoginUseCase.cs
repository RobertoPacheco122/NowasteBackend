using Nowaste.Communication.Requests.Auth;
using Nowaste.Communication.Responses.Users;

namespace Nowaste.Application.UseCases.Auth.Login;

public interface ILoginUseCase {
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
