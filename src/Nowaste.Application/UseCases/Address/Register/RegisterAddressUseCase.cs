using AutoMapper;
using Nowaste.Communication.Requests.Address;
using Nowaste.Communication.Responses.Address;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Address.Register;

public class RegisterAddressUseCase(
        IAddressWriteOnlyRepository addressWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRegisterAddressUseCase {
    private readonly IAddressWriteOnlyRepository _addressWriteOnlyRepository = addressWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseRegisteredAddressJson> Execute(RequestRegisterAddressJson request) {
        Validate(request);

        var addressEntity = _mapper.Map<AddressEntity>(request);
        addressEntity.CreatedAt = DateTime.UtcNow;

        await _addressWriteOnlyRepository.Add(addressEntity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredAddressJson {
            Id = addressEntity.Id,
            StreetName = addressEntity.StreetName,
        };
    }

    private static void Validate(RequestRegisterAddressJson request) {
        var validationResult = new RegisterAddressValidator().Validate(request);

        if (validationResult.IsValid is false) {
            var errorsMessages = validationResult.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
