using AutoMapper;
using Nowaste.Communication.Requests.Address;
using Nowaste.Communication.Requests.Establishment;
using Nowaste.Communication.Requests.Product;
using Nowaste.Communication.Requests.Users;
using Nowaste.Communication.Responses.Address;
using Nowaste.Communication.Responses.Establishment;
using Nowaste.Communication.Responses.Product;
using Nowaste.Domain.Entities;

namespace Nowaste.Application.AutoMapper;

public class AutoMapping : Profile {

    public AutoMapping() {
        RequestToEntity();
        EntityToResponse();
        RequestToRequest();
    }

    private void RequestToEntity() {
        CreateMap<RequestRegisterUserJson, UserEntity>();

        CreateMap<RequestRegisterUserJson, PersonEntity>();

        CreateMap<RequestRegisterEstablishmentJson, EstablishmentEntity>();

        CreateMap<RequestRegisterAddressJson, AddressEntity>();

        CreateMap<RequestRegisterProductJson, ProductEntity>();
        CreateMap<RequestRegisterProductCategoryJson, ProductCategoryEntity>();
        CreateMap<RequestUpdateProductPriceJson, ProductPriceHistoryEntity>();

        CreateMap<RequestRegisterOperatingDayJson, OperatingDayEntity>();
    }

    private void RequestToRequest() {

    }

    private void EntityToResponse() {
        CreateMap<AddressEntity, ResponseRegisteredAddressJson>();
        CreateMap<AddressEntity, ResponseGetAllAddressesJson>();
        CreateMap<AddressEntity, ResponseGetAddressByIdJson>();

        CreateMap<ProductEntity, ResponseGetAllProductsByEstablishmentJson>();
        CreateMap<ProductEntity, ResponseGetProductByIdJson>();
        CreateMap<ProductEntity, ResponseAvailableEstablishmentProductJson>();

        CreateMap<ProductPriceHistoryEntity, ResponseGetProductPriceByIdJson>();

        CreateMap<ProductCategoryEntity, ResponseGetAllProductCategoriesJson>();
        CreateMap<ProductCategoryEntity, ResponseRegisteredProductCategoryJson>();
        CreateMap<ProductCategoryEntity, ResponseGetProductCategoryByIdJson>();

        CreateMap<EstablishmentEntity, ResponseGetEstablishmentByIdJson>();
    }
}
 