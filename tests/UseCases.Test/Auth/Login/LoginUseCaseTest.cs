using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Auth;
using CommonTestUtilities.Token;
using Nowaste.Application.UseCases.Auth.Login;
using Nowaste.Domain.Entities;
using Nowaste.Exception.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Auth.Login;

public class LoginUseCaseTest {
    [Fact]
    public async Task Success() {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user, request.Password);

        var response = await useCase.Execute(request);

        response.ShouldNotBeNull();
        response.Name.ShouldBe(user.Person.FullName.Split(" ").First());
        response.Token.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_When_User_Not_Found() {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user, user.PasswordHash);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<InvalidLoginException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("Email ou senha inválidos."));
    }

    [Fact]
    public async Task Error_When_Password_Is_Not_Match() {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<InvalidLoginException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("Email ou senha inválidos."));
    }

    private static LoginUseCase CreateUseCase(UserEntity user, string? password = null) {
        var userRepository = new UserRepositoryBuilder().GetUserByEmail(user).Build();
        var passwordEncrypter = new PasswordEncrypterBuild().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();

        return new LoginUseCase(
            userRepository,
            passwordEncrypter,
            tokenGenerator
        );
    }
}
