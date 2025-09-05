using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Security.Cryptography;
using Nowaste.Domain.Security.Tokens;
using Nowaste.Infrastructure.DataAccess;
using WebApi.Test.Resources;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program> {
    public UserIdentityManager CustomerUser { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.UseEnvironment("Test")
            .ConfigureServices(services => {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<NowasteDbContext>(config => {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<NowasteDbContext>();
                var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                var tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passwordEncrypter, tokenGenerator);
            });
    }

    private void StartDatabase(
        NowasteDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator tokenGenerator
    ) {
        AddConsumerUser(dbContext, passwordEncrypter, tokenGenerator);

        dbContext.SaveChanges();
    }

    private UserEntity AddConsumerUser(
        NowasteDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator tokenGenerator
    ) {
        var user = UserBuilder.Build();
        var password = user.PasswordHash;

        user.PasswordHash = passwordEncrypter.Encrypt(user.PasswordHash);

        var token = tokenGenerator.Generate(user);

        var person = user.Person;
        person.User = user;
        person.UserId = user.Id;

        dbContext.Users.Add(user);
        dbContext.Persons.Add(person);

        CustomerUser = new UserIdentityManager(user, password, token);

        return user;
    }
}
