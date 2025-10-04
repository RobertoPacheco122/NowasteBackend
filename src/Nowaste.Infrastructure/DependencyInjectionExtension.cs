using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Domain.Repositories.Person;
using Nowaste.Domain.Repositories.User;
using Nowaste.Domain.Security.Cryptography;
using Nowaste.Domain.Security.Tokens;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Infrastructure.DataAccess;
using Nowaste.Infrastructure.DataAccess.Repositories.Address;
using Nowaste.Infrastructure.DataAccess.Repositories.Establishment;
using Nowaste.Infrastructure.DataAccess.Repositories.Person;
using Nowaste.Infrastructure.DataAccess.Repositories.User;
using Nowaste.Infrastructure.Extensions;
using Nowaste.Infrastructure.Security.Tokens;
using Nowaste.Infrastructure.Services.LoggedUser;

namespace Nowaste.Infrastructure;

public static class DependencyInjectionExtension {
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        AddRepositories(services);
        AddToken(services, configuration);

        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        if (configuration.IsTestEnvironment() is false)
            AddDbContext(services, configuration);
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration) {
        var expiratioinTimeInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var siginKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expiratioinTimeInMinutes, siginKey!));
    }

    private static void AddRepositories(IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

        services.AddScoped<IPersonReadOnlyRepository, PersonRepository>();
        services.AddScoped<IPersonWriteOnlyRepository, PersonRepository>();

        services.AddScoped<IAddressReadOnlyRepository, AddressRepository>();
        services.AddScoped<IAddressWriteOnlyRepository, AddressRepository>();
        services.AddScoped<IAddressUpdateOnlyRepository, AddressRepository>();

        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
    }

    private static void AddDbContext (IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<NowasteDbContext>(config => config.UseNpgsql(connectionString));
    }
}
