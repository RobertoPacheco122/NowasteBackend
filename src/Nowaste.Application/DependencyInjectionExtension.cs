using Microsoft.Extensions.DependencyInjection;
using Nowaste.Application.AutoMapper;
using Nowaste.Application.UseCases.Auth.Login;
using Nowaste.Application.UseCases.Persons.Register;
using Nowaste.Application.UseCases.Users.Register;

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
        services.AddScoped<IRegisterPersonUseCase, RegisterPersonUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
}
