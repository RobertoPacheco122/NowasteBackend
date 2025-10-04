using Microsoft.Extensions.DependencyInjection;
using Nowaste.Application.AutoMapper;
using Nowaste.Application.UseCases.Address.Delete;
using Nowaste.Application.UseCases.Address.GetAllByEstablishment;
using Nowaste.Application.UseCases.Address.GetAllByInstitution;
using Nowaste.Application.UseCases.Address.GetAllByPerson;
using Nowaste.Application.UseCases.Address.Register;
using Nowaste.Application.UseCases.Address.UpdateByEstablishment;
using Nowaste.Application.UseCases.Address.UpdateByInstitution;
using Nowaste.Application.UseCases.Address.UpdateByPerson;
using Nowaste.Application.UseCases.Auth.Login;
using Nowaste.Application.UseCases.Establishments.Register;
using Nowaste.Application.UseCases.Users.ChangePassword;
using Nowaste.Application.UseCases.Users.Register;
using Nowaste.Application.UseCases.Users.UpdateProfile;

namespace Nowaste.Application;

public static class DependencyInjectionExtension {
    public static void AddApplication(this IServiceCollection services) {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    public static void AddAutoMapper(IServiceCollection services) {
        services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapping>());
    }

    public static void AddUseCases(IServiceCollection services) {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IUpdateUserProfileUseCase, UpdateUserProfileUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();

        services.AddScoped<IRegisterAddressUseCase, RegisterAddressUseCase>();
        services.AddScoped<IGetAllAddressesByEstablishmentUseCase, GetAllAddressesByEstablishmentUseCase>();
        services.AddScoped<IGetAllAddressesByInstitutionUseCase, GetAllAddressesByInstitutionUseCase>();
        services.AddScoped<IGetAllAddressesByPersonUseCase, GetAllAddressesByPersonUseCase>();
        services.AddScoped<IUpdateAddressByEstablishmentUseCase, UpdateAddressByEstablishmentUseCase>();
        services.AddScoped<IUpdateAddressByInstitutionUseCase, UpdateAddressByInstitutionUseCase>();
        services.AddScoped<IUpdateAddressByPersonUseCase, UpdateAddressByPersonUseCase>();
        services.AddScoped<IDeleteAddressUseCase, DeleteAddressUseCase>();

        services.AddScoped<IRegisterEstablishmentUseCase, RegisterEstablishmentUseCase>();

        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
}
