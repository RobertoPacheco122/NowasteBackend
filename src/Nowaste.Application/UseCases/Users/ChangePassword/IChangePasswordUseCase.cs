using Nowaste.Communication.Requests.Users;

namespace Nowaste.Application.UseCases.Users.ChangePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}
