using Nowaste.Communication.Requests.Users;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.User;
using Nowaste.Domain.Security.Cryptography;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Users.ChangePassword;

public class ChangePasswordUseCase(
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        IPasswordEncrypter passwordEncrypter
    ) : IChangePasswordUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository = userUpdateOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter = passwordEncrypter;

    public async Task Execute(RequestChangePasswordJson request) {
        var authenticatedUser = await _loggedUser.Get();

        Validate(request, authenticatedUser);

        var useEntity = await _userUpdateOnlyRepository.GetById(authenticatedUser.Id);

        useEntity.PasswordHash = _passwordEncrypter.Encrypt(request.NewPassword);

        _userUpdateOnlyRepository.Update(useEntity);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, UserEntity loggedUser) {
        var result = new ChangePasswordValidator().Validate(request);

        var passwordMatch = _passwordEncrypter.Verify(request.OldPassword, loggedUser.PasswordHash);

        if (passwordMatch is false)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty,
                "A senha informada é diferente da senha atual.")
            );

        if (!result.IsValid) {
            var errorsMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
