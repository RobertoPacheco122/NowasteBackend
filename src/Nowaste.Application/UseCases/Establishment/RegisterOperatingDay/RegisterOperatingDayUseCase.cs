using AutoMapper;
using Nowaste.Communication.Requests.Establishment;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.RegisterOperatingDay;

public class RegisterOperatingDayUseCase(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IEstablishmentWriteOnlyRepository establishmentWriteOnlyRepository,
        IEstablishmentReadOnlyRepository establishmentReadOnlyRepository,
        IEstablishmentUpdateOnlyRepository establishmentUpdateOnlyRepository
    ) : IRegisterOperatingDayUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IEstablishmentWriteOnlyRepository _establishmentWriteOnlyRepository = establishmentWriteOnlyRepository;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository = establishmentReadOnlyRepository;
    private readonly IEstablishmentUpdateOnlyRepository _establishmentUpdateOnlyRepository = establishmentUpdateOnlyRepository;

    public async Task Execute(RequestRegisterOperatingDayJson request) {
        await Validate(request);

        var operatingDayEntity = _mapper.Map<OperatingDayEntity>(request);
        operatingDayEntity.CreatedAt = DateTime.UtcNow;

        var previousOperatingDayOnTheSameWeekDay = await _establishmentUpdateOnlyRepository
            .GetOperatingDayByDayOfWeek(request.EstablishmentId, request.DayOfWeek);

        if (previousOperatingDayOnTheSameWeekDay is not null) {
            previousOperatingDayOnTheSameWeekDay.OpeningTime = request.OpeningTime;
            previousOperatingDayOnTheSameWeekDay.ClosingTime = request.ClosingTime;
            previousOperatingDayOnTheSameWeekDay.UpdatedAt = DateTime.UtcNow;

            _establishmentUpdateOnlyRepository.UpdateOperatingDay(previousOperatingDayOnTheSameWeekDay);

            await _unitOfWork.Commit();

            return;
        }

        await _establishmentWriteOnlyRepository.AddOperatingDay(operatingDayEntity);

        await _unitOfWork.Commit();
    }

    public async Task Validate(RequestRegisterOperatingDayJson request) {
        var validationResult = new RegisterOperatingDayValidator().Validate(request);

        var establishmentExists = await _establishmentReadOnlyRepository.ExistActiveWithId(request.EstablishmentId);

        if (establishmentExists is false)
            throw new NotFoundException("Estabelecimento não encontrado.");

        if (validationResult.IsValid is false) {
            var errorsMessages = validationResult.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
