using Nowaste.Communication.Requests.Establishment;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.Update;

internal class UpdateEstablishmentUseCase(
    IUnitOfWork unitOfWork,
    IEstablishmentReadOnlyRepository establishmentReadOnlyRepository,
    IEstablishmentUpdateOnlyRepository establishmentUpdateOnlyRepository
) : IUpdateEstablishmentUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository =
        establishmentReadOnlyRepository;
    private readonly IEstablishmentUpdateOnlyRepository _establishmentUpdateOnlyRepository =
        establishmentUpdateOnlyRepository;

    public async Task Execute(Guid establishmentId, RequestUpdateEstablishmentJson request)
    {
        Validate(request);

        var establishmentEntity =
            await _establishmentUpdateOnlyRepository.GetById(establishmentId)
            ?? throw new NotFoundException("Estabelecimento não encontrado.");

        establishmentEntity.ExhibitionName = request.ExhibitionName;
        establishmentEntity.Email = request.Email;
        establishmentEntity.Telephone = request.Telephone;
        establishmentEntity.PhoneNumber = request.PhoneNumber;
        establishmentEntity.Status = (Domain.Enums.EEstablishmentStatus)request.Status;

        establishmentEntity.UpdatedAt = DateTime.UtcNow;

        _establishmentUpdateOnlyRepository.Update(establishmentEntity);

        await _unitOfWork.Commit();
    }

    public static void Validate(RequestUpdateEstablishmentJson request)
    {
        var validationResult = new UpdateEstablishmentValidator().Validate(request);

        if (validationResult.IsValid is false)
        {
            var errorsMessages = validationResult.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
