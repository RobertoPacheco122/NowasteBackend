using AutoMapper;
using Nowaste.Communication.Enums;
using Nowaste.Communication.Requests.Address;
using Nowaste.Communication.Responses.Address;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Address.Register;

public class RegisterAddressUseCase(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IAddressWriteOnlyRepository addressWriteOnlyRepository,
    IAddressUpdateOnlyRepository addressUpdateOnlyRepository
) : IRegisterAddressUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IAddressWriteOnlyRepository _addressWriteOnlyRepository =
        addressWriteOnlyRepository;
    private readonly IAddressUpdateOnlyRepository _addressUpdateOnlyRepository =
        addressUpdateOnlyRepository;

    public async Task<ResponseRegisteredAddressJson> Execute(RequestRegisterAddressJson request)
    {
        Validate(request);

        var allAddresses = await GetAllAddressesBasedOnEntityType(request);

        HandleAddressTypesConflict(allAddresses, request, EAddressType.Home);
        HandleAddressTypesConflict(allAddresses, request, EAddressType.Operational);
        HandleAddressTypesConflict(allAddresses, request, EAddressType.Work);

        var addressEntity = _mapper.Map<AddressEntity>(request);
        addressEntity.CreatedAt = DateTime.UtcNow;

        await _addressWriteOnlyRepository.Add(addressEntity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredAddressJson
        {
            Id = addressEntity.Id,
            StreetName = addressEntity.StreetName,
        };
    }

    private static void Validate(RequestRegisterAddressJson request)
    {
        var validationResult = new RegisterAddressValidator().Validate(request);

        if (validationResult.IsValid is false)
        {
            var errorsMessages = validationResult.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }

    private async Task<ICollection<AddressEntity>> GetAllAddressesBasedOnEntityType(
        RequestRegisterAddressJson request
    )
    {
        return request.EstablishmentId.HasValue
                ? await _addressUpdateOnlyRepository.GetAllByEstablishment(
                    request.EstablishmentId.Value
                )
            : request.InstitutionId.HasValue
                ? await _addressUpdateOnlyRepository.GetAllByInstitution(
                    request.InstitutionId.Value
                )
            : request.PersonId.HasValue
                ? await _addressUpdateOnlyRepository.GetAllByPerson(request.PersonId.Value)
            : Array.Empty<AddressEntity>();
    }

    private void HandleAddressTypesConflict(
        ICollection<AddressEntity> allAddresses,
        RequestRegisterAddressJson request,
        EAddressType targetAddressType
    )
    {
        if (request.AddressType != targetAddressType)
            return;

        var previousTargetAddress = allAddresses.FirstOrDefault(f =>
            f.AddressType == (Domain.Enums.EAddressType)targetAddressType
        );

        if (previousTargetAddress is null)
            return;

        previousTargetAddress.AddressType = Domain.Enums.EAddressType.Common;
        previousTargetAddress.UpdatedAt = DateTime.UtcNow;

        _addressUpdateOnlyRepository.Update(previousTargetAddress);
    }
}
