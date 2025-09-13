using AutoMapper;
using Nowaste.Application.UseCases.Address.Register;
using Nowaste.Communication.Requests.Establishments;
using Nowaste.Communication.Responses.Establishments;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishments.Register;

public class RegisterEstablishmentUseCase(
        IEstablishmentRepository establishmentsRepository,
        IAddressWriteOnlyRepository addressWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRegisterEstablishmentUseCase {
    private readonly IEstablishmentRepository _establishmentRepository = establishmentsRepository;    
    private readonly IAddressWriteOnlyRepository _addressWriteOnlyRepository = addressWriteOnlyRepository;    
    private readonly IUnitOfWork _unitOfWork = unitOfWork;    
    private readonly IMapper _mapper = mapper;    

    public async Task<ResponseRegisteredEstablishmentJson> Execute(RequestRegisterEstablishmentJson request) {
        await Validate(request);

        var establishmentEntity = _mapper.Map<EstablishmentEntity>(request);
        establishmentEntity.CreatedAt = DateTime.UtcNow;

        await _establishmentRepository.Add(establishmentEntity);

        var addressEntity = _mapper.Map<AddressEntity>(request.OperationAddress);
        addressEntity.CreatedAt = DateTime.UtcNow;
        addressEntity.EstablishmentId = establishmentEntity.Id;
        addressEntity.AddressType = EAddressType.Operational;

        await _addressWriteOnlyRepository.Add(addressEntity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredEstablishmentJson {
            ExhibitionName = establishmentEntity.ExhibitionName,
            Id = establishmentEntity.Id,
        };
    }

    private async Task Validate(RequestRegisterEstablishmentJson request) {
        var establishmentValidationResult = new RegisterEstablishmentValidator().Validate(request);
        var addressValidationResult = new RegisterAddressValidator().Validate(request.OperationAddress);

        var cnpjOrEmailAlreadyExist = await _establishmentRepository.ExistActiveEstablishmentWithCnpjOrEmail(request.Email, request.Cnpj);

        if (cnpjOrEmailAlreadyExist)
            establishmentValidationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty,
                "Já existe um estabelecimento cadastrado com este CNPJ ou email.")
            );

        if (establishmentValidationResult.IsValid is false || addressValidationResult.IsValid is false) {
            var establishmentErrorsMessages = establishmentValidationResult.Errors.Select(f => f.ErrorMessage).ToList();
            var addressErrorsMessages = addressValidationResult.Errors.Select(f => f.ErrorMessage).ToList();

            var errorsMessages = new List<string>();

            errorsMessages.AddRange(establishmentErrorsMessages);
            errorsMessages.AddRange(addressErrorsMessages);

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
} 
