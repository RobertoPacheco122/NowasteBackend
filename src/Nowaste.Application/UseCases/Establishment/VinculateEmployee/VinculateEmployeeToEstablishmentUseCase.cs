using Nowaste.Communication.Requests.Establishment;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Domain.Repositories.User;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.VinculateEmployee;

public class VinculateEmployeeToEstablishmentUseCase(
    IUnitOfWork unitOfWork,
    IUserUpdateOnlyRepository userUpdateOnlyRepository,
    IEstablishmentReadOnlyRepository establishmentReadOnlyRepository
) : IVinculateEmployeeToEstablishmentUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository = userUpdateOnlyRepository;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository =
        establishmentReadOnlyRepository;

    public async Task Execute(RequestVinculateEmployeeToEstablishmentJson request)
    {
        Validate(request);

        var userToVinculateEntity =
            await _userUpdateOnlyRepository.GetById(request.UserId)
            ?? throw new NotFoundException("Usuário não encontrado.");

        var existEstablishmentWithGivenId =
            await _establishmentReadOnlyRepository.ExistActiveWithId(request.EstablishmentId);

        if (existEstablishmentWithGivenId is false)
            throw new NotFoundException("Estabelecimento não encontrado");

        userToVinculateEntity.Role = request.Role;
        userToVinculateEntity.Person.EstablishmentId = request.EstablishmentId;

        _userUpdateOnlyRepository.Update(userToVinculateEntity);

        await _unitOfWork.Commit();
    }

    public static void Validate(RequestVinculateEmployeeToEstablishmentJson request)
    {
        var validationResult = new VinculateEmployeeToEstablishmentValidator().Validate(request);

        if (validationResult.IsValid is false)
        {
            var errorsMessages = validationResult.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
