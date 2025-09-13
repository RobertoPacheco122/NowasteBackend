using Nowaste.Communication.Requests.Users;

namespace Nowaste.Application.UseCases.Users.UpdateProfile;

public interface IUpdateUserProfileUseCase {
    public Task Execute(RequestUpdateUserProfileJson request);
}
