using AutoMapper;
using Nowaste.Application.UseCases.Address.Register;
using Nowaste.Communication.Requests.Establishment;
using Nowaste.Communication.Responses.Establishment;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Domain.Repositories.User;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.Register;

public class RegisterEstablishmentUseCase(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        IEstablishmentWriteOnlyRepository establishmentWriteOnlyRepository,
        IEstablishmentReadOnlyRepository establishmentReadOnlyRepository,
        IAddressWriteOnlyRepository addressWriteOnlyRepository
    ) : IRegisterEstablishmentUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository = userUpdateOnlyRepository;
    private readonly IEstablishmentWriteOnlyRepository _establishmentWriteOnlyRepository = establishmentWriteOnlyRepository;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository = establishmentReadOnlyRepository;
    private readonly IAddressWriteOnlyRepository _addressWriteOnlyRepository = addressWriteOnlyRepository;

    public async Task<ResponseRegisteredEstablishmentJson> Execute(RequestRegisterEstablishmentJson request) {
        await Validate(request);

        var establishmentEntity = _mapper.Map<EstablishmentEntity>(request);
        establishmentEntity.CreatedAt = DateTime.UtcNow;

        await _establishmentWriteOnlyRepository.Add(establishmentEntity);

        var addressEntity = _mapper.Map<AddressEntity>(request.OperationAddress);
        addressEntity.CreatedAt = DateTime.UtcNow;
        addressEntity.EstablishmentId = establishmentEntity.Id;
        addressEntity.AddressType = EAddressType.Operational;

        await _addressWriteOnlyRepository.Add(addressEntity);

        var loggedUser = await _loggedUser.Get();

        var userEntity = await _userUpdateOnlyRepository.GetById(loggedUser.Id) ??
            throw new NotFoundException("Usuário não encontrado.");

        userEntity.Role = Roles.ESTABLISHMENT_ADMIN;
        userEntity.Person.EstablishmentId = establishmentEntity.Id;
        userEntity.UpdatedAt = DateTime.UtcNow;

        _userUpdateOnlyRepository.Update(userEntity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredEstablishmentJson {
            ExhibitionName = establishmentEntity.ExhibitionName,
            Id = establishmentEntity.Id,
        };
    }

    private async Task Validate(RequestRegisterEstablishmentJson request) {
        var establishmentValidationResult = new RegisterEstablishmentValidator().Validate(request);

        var addressValidationResult = request.OperationAddress is not null
            ? new RegisterAddressValidator().Validate(request.OperationAddress)
            : new FluentValidation.Results.ValidationResult(new List<FluentValidation.Results.ValidationFailure> {
                new(nameof(request.OperationAddress), "O endereço de operação é obrigatório.")
            });

        var cnpjOrEmailAlreadyExist = await _establishmentReadOnlyRepository.ExistActiveEstablishmentWithCnpjOrEmail(request.Email, request.Cnpj);

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
