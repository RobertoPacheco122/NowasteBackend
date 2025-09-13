using Microsoft.Extensions.DependencyInjection;
using Nowaste.Application.AutoMapper;
using Nowaste.Application.UseCases.Address.Register;
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

        services.AddScoped<IRegisterEstablishmentUseCase, RegisterEstablishmentUseCase>();

        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
}
