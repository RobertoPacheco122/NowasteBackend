using AutoMapper;
using Nowaste.Communication.Responses.Address;
using Nowaste.Communication.Responses.Establishment;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.GetById;

public class GetEstablishmentByIdUseCase(
    IMapper mapper,
    IEstablishmentReadOnlyRepository establishmentReadOnlyRepository
) : IGetEstablishmentByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository =
        establishmentReadOnlyRepository;

    public async Task<ResponseGetEstablishmentByIdJson> Execute(Guid establishmentId)
    {
        var establishmentEntity =
            await _establishmentReadOnlyRepository.GetById(establishmentId)
            ?? throw new NotFoundException("Estabelecimento não encontrado.");

        var formattedEstablishment = _mapper.Map<ResponseGetEstablishmentByIdJson>(
            establishmentEntity
        );

        var averageRating = establishmentEntity.Reviews.Average(review => review.Rating);
        var totalReviews = establishmentEntity.Reviews.Count;
        var operationalAddress = establishmentEntity.Addresses.First(address =>
            address.AddressType == EAddressType.Operational
        );

        formattedEstablishment.TotalReviews = totalReviews;
        formattedEstablishment.AverageRating = averageRating;
        formattedEstablishment.OperationalAddress = _mapper.Map<ResponseGetAddressByIdJson>(
            operationalAddress
        );

        return formattedEstablishment;
    }
}
