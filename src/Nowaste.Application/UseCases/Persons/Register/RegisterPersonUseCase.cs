using AutoMapper;
using Nowaste.Communication.Requests.Persons;
using Nowaste.Communication.Responses.Persons;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Persons;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Persons.Register;

public class RegisterPersonUseCase(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPersonRepository personsRepository
    ) : IRegisterPersonUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IPersonRepository _personsRepository = personsRepository;

    public async Task<ResponseRegisteredPersonJson> Execute(RequestRegisterPersonJson request) {
        Validate(request);

        var personEntity = _mapper.Map<PersonEntity>(request);
        personEntity.CreatedAt = DateTime.UtcNow;

        await _personsRepository.Add(personEntity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredPersonJson>(personEntity);
    }

    private static void Validate(RequestRegisterPersonJson request) {
        var result = new RegisterPersonValidator().Validate(request);

        if (!result.IsValid) {
            var errorsMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
