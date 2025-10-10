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
    ) : IGetAvailableEstablishmentForAddressUseCase {
    private readonly IMapper _mapper = mapper;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository = establishmentReadOnlyRepository;
    private readonly IAddressReadOnlyRepository _addressReadOnlyRepository = addressReadOnlyRepository;

    public async Task<ICollection<ResponseGetAllAvailableEstablishmentForAddressJson>> Execute(Guid addressId) {
        var existActiveAddressWithId = await _addressReadOnlyRepository.ExistActiveWithId(addressId);

        if (existActiveAddressWithId is false)
            throw new NotFoundException("Endereço não encontrado.");

        var establishmentsEntities = await _establishmentReadOnlyRepository
            .GetAllAvailableForAddress(addressId);

        if(establishmentsEntities.Count is 0)
            throw new NotFoundException("Nenhum estabelecimento disponível para o endereço informado.");

        return [..establishmentsEntities
            .Select(establishment => {
                var formattedProducts = new List<ResponseAvailableEstablishmentProductJson>();

                foreach (var product in establishment.Products) {
                    if (product.IsActive is false) continue;

                    var actualPriceHistory = product.PriceHistories
                        .Where(priceHistory => priceHistory.EffectiveDate <= DateTime.UtcNow)
                        .OrderByDescending(priceHistory => priceHistory.EffectiveDate)
                        .FirstOrDefault();

                    if (actualPriceHistory is null) continue;

                    formattedProducts.Add(new ResponseAvailableEstablishmentProductJson {
                        ActualPriceHistory = _mapper.Map<ResponseGetProductPriceByIdJson>(actualPriceHistory),
                        Description = product.Description,
                        Id = product.Id,
                        InventoryTrackingType = (EProductInventoryTrackingType)product.InventoryTrackingType,
                        IsActive = product.IsActive,
                        Name = product.Name,
                        ProductCategory = _mapper.Map<ResponseGetProductCategoryByIdJson>(product.ProductCategory),
                        QuantityInStock = product.QuantityInStock,
                    });
                }
              
                return new ResponseGetAllAvailableEstablishmentForAddressJson {
                    Establishment = _mapper.Map<ResponseGetEstablishmentByIdJson>(establishment),
                    Products = formattedProducts
                };
            })];

    }
}
