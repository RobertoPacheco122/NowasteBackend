using Nowaste.Application.UseCases.Address.Register;
using Nowaste.Communication.Enums;
using Nowaste.Communication.Requests.Address;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Address.UpdateByPerson;

public class UpdateAddressByPersonUseCase(
    IUnitOfWork unitOfWork,
    IAddressUpdateOnlyRepository addressUpdateOnlyRepository
) : IUpdateAddressByPersonUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAddressUpdateOnlyRepository _addressUpdateOnlyRepository =
        addressUpdateOnlyRepository;

    public async Task Execute(Guid addressId, RequestRegisterAddressJson request)
    {
        Validate(request);

        var addressEntity =
            await _addressUpdateOnlyRepository.GetById(addressId)
            ?? throw new NotFoundException("Address not found");

        if (addressEntity.EstablishmentId != request.EstablishmentId)
            throw new NotFoundException("Address not found");

        var allEstablishmentAddresses = await _addressUpdateOnlyRepository.GetAllByEstablishment(
            addressEntity.EstablishmentId!.Value
        );

        addressEntity.StreetName = request.StreetName;
        addressEntity.Number = request.Number;
        addressEntity.Complement = request.Complement;
        addressEntity.Neighborhood = request.Neighborhood;
        addressEntity.City = request.City;
        addressEntity.State = request.State;
        addressEntity.ZipCode = request.ZipCode;
        addressEntity.Latitude = request.Latitude;
        addressEntity.Longitude = request.Longitude;
        addressEntity.AddressType = (Domain.Enums.EAddressType)request.AddressType;
        addressEntity.PersonId = request.PersonId;
        addressEntity.EstablishmentId = request.EstablishmentId;
        addressEntity.InstitutionId = request.InstitutionId;

        addressEntity.UpdatedAt = DateTime.UtcNow;

        var previousWorkAddress = allEstablishmentAddresses.FirstOrDefault(a =>
            a.AddressType == Domain.Enums.EAddressType.Operational
        );

        var previousHomeAddress = allEstablishmentAddresses.FirstOrDefault(a =>
            a.AddressType == Domain.Enums.EAddressType.Home
        );

        if (request.AddressType == EAddressType.Work && previousWorkAddress is not null)
        {
            previousWorkAddress.AddressType = Domain.Enums.EAddressType.Common;

            _addressUpdateOnlyRepository.Update(previousWorkAddress);
        }

        if (request.AddressType == EAddressType.Home && previousHomeAddress is not null)
        {
            previousHomeAddress.AddressType = Domain.Enums.EAddressType.Common;

            _addressUpdateOnlyRepository.Update(previousHomeAddress);
        }

        _addressUpdateOnlyRepository.Update(addressEntity);

        await _unitOfWork.Commit();
    }

    private static void Validate(RequestRegisterAddressJson request)
    {
        var validationResult = new RegisterAddressValidator().Validate(request);

        if (request.EstablishmentId is null)
            validationResult.Errors.Add(
                new FluentValidation.Results.ValidationFailure(
                    string.Empty,
                    "O personId é obrigatório."
                )
            );

        if (validationResult.IsValid is false)
        {
            var errorsMessages = validationResult.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
