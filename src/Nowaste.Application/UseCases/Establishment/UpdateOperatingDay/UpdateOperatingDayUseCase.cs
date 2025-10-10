using Nowaste.Communication.Requests.Establishment;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.UpdateOperatingDay;

public class UpdateOperatingDayUseCase(
        IUnitOfWork unitOfWork,
        IEstablishmentUpdateOnlyRepository establishmentUpdateOnlyRepository
    ) : IUpdateOperatingDayUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEstablishmentUpdateOnlyRepository _establishmentUpdateOnlyRepository = establishmentUpdateOnlyRepository;

    public async Task Execute(Guid operatingDayId, RequestUpdateOperatingDayJson request) {
        Validate(request);

        var operatingDayEntity = await _establishmentUpdateOnlyRepository.GetOperatingDayById(operatingDayId) ??
            throw new NotFoundException("Dia de operação não encontrado.");

        operatingDayEntity.OpeningTime = request.OpeningTime;
        operatingDayEntity.ClosingTime = request.ClosingTime;
        operatingDayEntity.UpdatedAt = DateTime.UtcNow;

        _establishmentUpdateOnlyRepository.UpdateOperatingDay(operatingDayEntity);

        await _unitOfWork.Commit();
    }

    public static void Validate(RequestUpdateOperatingDayJson request) {
        var validationResult = new UpdateOperatingDayValidator().Validate(request);

        if (validationResult.IsValid is false) {
            var errorsMessages = validationResult.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
