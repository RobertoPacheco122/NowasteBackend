using AutoMapper;
using Nowaste.Communication.Responses.Address;
using Nowaste.Communication.Responses.Product;
using Nowaste.Domain.Repositories.Product;

namespace Nowaste.Application.UseCases.Product.GetById;

public class GetProductByIdUseCase(
    IMapper mapper,
    IProductReadOnlyRepository productReadOnlyRepository
) : IGetProductByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository =
        productReadOnlyRepository;

    public async Task<ResponseGetProductByIdJson> Execute(Guid id)
    {
        var productEntity =
            await _productReadOnlyRepository.GetById(id)
            ?? throw new KeyNotFoundException("Product not found");

        var actualPriceHistory = productEntity
            .PriceHistories.Where(priceHistory => priceHistory.EffectiveDate <= DateTime.UtcNow)
            .OrderByDescending(priceHistory => priceHistory.EffectiveDate)
            .FirstOrDefault();

        var formattedActualPriceHistory = _mapper.Map<ResponseGetProductPriceByIdJson>(
            actualPriceHistory
        );

        var operationalAddress = productEntity.Establishment.Addresses.FirstOrDefault(address =>
            address.AddressType == Domain.Enums.EAddressType.Operational
        );

        var result = _mapper.Map<ResponseGetProductByIdJson>(productEntity);
        result.ActualPriceHistory = formattedActualPriceHistory;
        result.Establishment.AverageRating = productEntity.Establishment.Reviews.Average(review =>
            review.Rating
        );
        ;
        result.Establishment.TotalReviews = productEntity.Establishment.Reviews.Count;
        result.Establishment.OperationalAddress = _mapper.Map<ResponseGetAddressByIdJson>(
            operationalAddress
        );

        return result;
    }
}
