using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Nowaste.Application.AutoMapper;
using Nowaste.Communication.Requests.Users;
using Nowaste.Domain.Entities;

namespace CommonTestUtilities.Mapper;

public class MapperBuilder {
    public static IMapper Build() {
        var configuration = new MapperConfiguration(config => {
            config.AddProfile(new AutoMapping());
            config.CreateMap<RequestRegisterUserJson, UserEntity>()
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => new PersonEntity {
                    BirthDate = src.BirthDate,
                    Cpf = src.Cpf,
                    PhoneNumber = src.PhoneNumber,
                    FullName = src.FullName
                }));
        }, NullLoggerFactory.Instance);

        return configuration.CreateMapper();
    }
}
