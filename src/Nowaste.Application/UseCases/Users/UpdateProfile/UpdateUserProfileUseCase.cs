using Nowaste.Communication.Requests.Users;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Person;
using Nowaste.Domain.Repositories.User;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;
using System.Threading.Tasks;

namespace Nowaste.Application.UseCases.Users.UpdateProfile;

public class UpdateUserProfileUseCase(
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        IPersonReadOnlyRepository personReadOnlyRepository
    ) : IUpdateUserProfileUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository = userUpdateOnlyRepository;
    private readonly IPersonReadOnlyRepository _personReadOnlyRepository = personReadOnlyRepository;

    public async Task Execute(RequestUpdateUserProfileJson request) {
        await Validate(request);

        var authenticatedUser = await _loggedUser.Get();

        var userEntity = await _userUpdateOnlyRepository.GetById(authenticatedUser.Id);

        userEntity.Role = request.Role;
        userEntity.UserStatus = (Domain.Enums.EUserStatus)request.UserStatus;
        userEntity.Person.Nickname = request.Nickname;
        userEntity.Person.PhoneNumber = request.PhoneNumber;
        userEntity.Person.InstitutionId = request.InstitutionId;
        userEntity.Person.EstablishmentId = request.EstablishmentId;

        userEntity.UpdatedAt = DateTime.UtcNow;

        _userUpdateOnlyRepository.Update(userEntity);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserProfileJson request) {
        var result = new UpdateUserProfileValidator().Validate(request);

        var phoneNumberExist = await _personReadOnlyRepository.ExistActiveUserWithPhoneNumber(request.PhoneNumber);

        if (phoneNumberExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty,
                "Já existe um usuário cadastrado com este número de celular.")
            );

        if (result.IsValid is false) {
            var errorsMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
