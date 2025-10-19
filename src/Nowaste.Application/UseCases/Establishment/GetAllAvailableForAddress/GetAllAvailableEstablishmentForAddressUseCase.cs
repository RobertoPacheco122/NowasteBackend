using AutoMapper;
using Nowaste.Communication.Enums;
using Nowaste.Communication.Responses.Establishment;
using Nowaste.Communication.Responses.Product;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.GetAllAvailableForAddress;

public class GetAllAvailableEstablishmentForAddressUseCase(
    IMapper mapper,
    IEstablishmentReadOnlyRepository establishmentReadOnlyRepository,
    IAddressReadOnlyRepository addressReadOnlyRepository
) : IGetAvailableEstablishmentForAddressUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository =
        establishmentReadOnlyRepository;
    private readonly IAddressReadOnlyRepository _addressReadOnlyRepository =
        addressReadOnlyRepository;

    public async Task<ICollection<ResponseGetAllAvailableEstablishmentForAddressJson>> Execute(
        Guid addressId
    )
    {
        var addressEntity =
            await _addressReadOnlyRepository.GetById(addressId)
            ?? throw new NotFoundException("Endereço não encontrado.");

        var establishmentsEntities =
            await _establishmentReadOnlyRepository.GetAllAvailableForAddress(addressId);

        if (establishmentsEntities.Count is 0)
            throw new NotFoundException(
                "Nenhum estabelecimento disponível para o endereço informado."
            );

        return
        [
            .. establishmentsEntities.Select(establishment =>
            {
                var formattedProducts = new List<ResponseAvailableEstablishmentProductJson>();

                foreach (var product in establishment.Products)
                {
                    if (product.IsActive is false)
                        continue;

                    var actualPriceHistory = product
                        .PriceHistories.Where(priceHistory =>
                            priceHistory.EffectiveDate <= DateTime.UtcNow
                        )
                        .OrderByDescending(priceHistory => priceHistory.EffectiveDate)
                        .FirstOrDefault();

                    if (actualPriceHistory is null)
                        continue;

                    formattedProducts.Add(
                        new ResponseAvailableEstablishmentProductJson
                        {
                            ActualPriceHistory = _mapper.Map<ResponseGetProductPriceByIdJson>(
                                actualPriceHistory
                            ),
                            Description = product.Description,
                            Id = product.Id,
                            InventoryTrackingType = (EProductInventoryTrackingType)
                                product.InventoryTrackingType,
                            IsActive = product.IsActive,
                            Name = product.Name,
                            ProductCategory = _mapper.Map<ResponseGetProductCategoryByIdJson>(
                                product.ProductCategory
                            ),
                            QuantityInStock = product.QuantityInStock,
                        }
                    );
                }

                var operationalAddress = establishment.Addresses.First(address =>
                    address.AddressType == Domain.Enums.EAddressType.Operational
                );

                var distanceFromAddressToEstablishmentInMeters = CalculateDistanceInMeters(
                    operationalAddress.Longitude,
                    operationalAddress.Latitude,
                    addressEntity.Longitude,
                    addressEntity.Latitude
                );

                var averageRating = establishment.Reviews.Count is 0
                    ? 0.0
                    : establishment.Reviews.Average(review => review.Rating);

                var formattedEstablishment = _mapper.Map<ResponseAvailableEstablishmentJson>(
                    establishment
                );
                formattedEstablishment.AverageRating = averageRating;
                formattedEstablishment.DistanceInMetersFromAddressToEstablishment =
                    distanceFromAddressToEstablishmentInMeters;

                return new ResponseGetAllAvailableEstablishmentForAddressJson
                {
                    Establishment = formattedEstablishment,
                    Products = formattedProducts,
                };
            }),
        ];
    }

    private static double CalculateDistanceInMeters(
        double lon1,
        double lat1,
        double lon2,
        double lat2
    )
    {
        const double R = 6371000;

        var latRad1 = ToRadians(lat1);
        var latRad2 = ToRadians(lat2);
        var deltaLat = ToRadians(lat2 - lat1);
        var deltaLon = ToRadians(lon2 - lon1);

        var a =
            Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2)
            + Math.Cos(latRad1)
                * Math.Cos(latRad2)
                * Math.Sin(deltaLon / 2)
                * Math.Sin(deltaLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        var distance = R * c;

        return distance;
    }

    private static double ToRadians(double degrees) => degrees * (Math.PI / 180.0);
}
