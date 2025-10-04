using AutoMapper;
using Nowaste.Communication.Requests.Address;
using Nowaste.Communication.Requests.Establishments;
using Nowaste.Communication.Requests.Users;
using Nowaste.Communication.Responses.Address;
using Nowaste.Communication.Responses.Establishments;
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
    }

    private void RequestToRequest() {

    }

    private void EntityToResponse() {
        CreateMap<AddressEntity, ResponseRegisteredAddressJson>();
        CreateMap<AddressEntity, ResponseGetAllAddressesJson>();
    }
}
 