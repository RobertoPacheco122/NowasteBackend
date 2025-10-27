using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Domain.Repositories.Person;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Domain.Repositories.Review;
using Nowaste.Domain.Repositories.User;
using Nowaste.Domain.Security.Cryptography;
using Nowaste.Domain.Security.Tokens;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Infrastructure.DataAccess;
using Nowaste.Infrastructure.DataAccess.Repositories.Address;
using Nowaste.Infrastructure.DataAccess.Repositories.Establishment;
using Nowaste.Infrastructure.DataAccess.Repositories.Order;
using Nowaste.Infrastructure.DataAccess.Repositories.Person;
using Nowaste.Infrastructure.DataAccess.Repositories.Product;
using Nowaste.Infrastructure.DataAccess.Repositories.Review;
using Nowaste.Infrastructure.DataAccess.Repositories.User;
using Nowaste.Infrastructure.Extensions;
using Nowaste.Infrastructure.Security.Tokens;
using Nowaste.Infrastructure.Services.LoggedUser;

namespace Nowaste.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        AddRepositories(services);
        AddToken(services, configuration);

        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        if (configuration.IsTestEnvironment() is false)
            AddDbContext(services, configuration);
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expiratioinTimeInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var siginKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(
            expiratioinTimeInMinutes,
            siginKey!
        ));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserWriteOnlyRepository, UserWriteOnlyRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserUpdateOnlyRepository>();

        services.AddScoped<IPersonWriteOnlyRepository, PersonWriteOnlyRepository>();
        services.AddScoped<IPersonReadOnlyRepository, PersonReadOnlyRepository>();

        services.AddScoped<IAddressWriteOnlyRepository, AddressWriteOnlyRepository>();
        services.AddScoped<IAddressReadOnlyRepository, AddressReadOnlyRepository>();
        services.AddScoped<IAddressUpdateOnlyRepository, AddressUpdateOnlyRepository>();

        services.AddScoped<IEstablishmentWriteOnlyRepository, EstablishmentWriteOnlyRepository>();
        services.AddScoped<IEstablishmentReadOnlyRepository, EstablishmentReadOnlyRepository>();
        services.AddScoped<IEstablishmentUpdateOnlyRepository, EstablishmentUpdateOnlyRepository>();

        services.AddScoped<IProductWriteOnlyRepository, ProductWriteOnlyRepository>();
        services.AddScoped<IProductReadOnlyRepository, ProductReadOnlyRepository>();
        services.AddScoped<IProductUpdateOnlyRepository, ProductUpdateOnlyRepository>();

        services.AddScoped<IOrderWriteOnlyRepository, OrderWriteOnlyRepository>();
        services.AddScoped<IOrderReadOnlyRepository, OrderReadOnlyRepository>();
        services.AddScoped<IOrderUpdateOnlyRepository, OrderUpdateOnlyRepository>();

        services.AddScoped<IReviewWriteOnlyRepository, ReviewWriteOnlyRepository>();
        services.AddScoped<IReviewReadOnlyRepository, ReviewReadOnlyRepository>();
        services.AddScoped<IReviewUpdateOnlyRepository, ReviewUpdateOnlyRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<NowasteDbContext>(config => config.UseNpgsql(connectionString));
    }
}
