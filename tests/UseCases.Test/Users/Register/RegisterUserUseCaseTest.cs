using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Token;
using Nowaste.Application.UseCases.Users.Register;
using Nowaste.Exception.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Users.Register;

public class RegisterUserUseCaseTest {
    [Fact]
    public async Task Success() {
        var useCase = CreateUseCase();

        var request = RequestRegisterUserJsonBuilder.Build();

        var response = await useCase.Execute(request);

        response.ShouldNotBeNull();
        response.Name.ShouldBe(request.FullName.Split(" ").First());
        response.Token.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_When_Email_Is_Empty() {
        var useCase = CreateUseCase();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O email é obrigatório."));
    }

    [Fact]
    public async Task Error_When_Email_Already_Exist() {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("Já existe um usuário cadastrado com este email."));
    }

    private static RegisterUserUseCase CreateUseCase(string? email = null) {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userRepository = new UserRepositoryBuilder();
        var personRepository = new PersonRepositoryBuilder().Build();
        var passwordEncrypter = new PasswordEncrypterBuild().Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();

        if (string.IsNullOrWhiteSpace(email) is false)
            userRepository.ExistActiveUserWithEmail(email);

        return new RegisterUserUseCase(
            unitOfWork,
            mapper,
            userRepository.Build(),
            personRepository,
            passwordEncrypter,
            tokenGenerator
        );
    }
}
