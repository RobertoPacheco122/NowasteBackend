using AutoMapper;
using Nowaste.Communication.Requests.Persons;
using Nowaste.Communication.Requests.Users;
using Nowaste.Communication.Responses.Persons;
using Nowaste.Communication.Responses.Users;
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
        CreateMap<RequestRegisterPersonJson, PersonEntity>();
        
    }

    private void RequestToRequest() {
        CreateMap<RequestRegisterUserJson, RequestRegisterPersonJson>();
    }

    private void EntityToResponse() {
        CreateMap<PersonEntity, ResponseRegisteredPersonJson>();
    }
}
 