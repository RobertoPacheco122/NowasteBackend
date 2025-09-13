using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Person;
using CommonTestUtilities.Repositories.User;
using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Token;
using Nowaste.Application.UseCases.Users.Register;
using Nowaste.Exception.ExceptionBase;
using Shouldly;

namespace UseCases.Test.User.Register;

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
        var useCase = CreateUseCase(email: request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("Já existe um usuário cadastrado com este email."));
    }

    [Fact]
    public async Task Error_When_Phone_Number_Already_Exist() {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(phoneNumber: request.PhoneNumber);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("Já existe um usuário cadastrado com este número de celular."));
    }

    private static RegisterUserUseCase CreateUseCase(string? email = null, string? phoneNumber = null) {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        var userWriteOnlyRepository = new UserWriteOnlyRepositoryBuilder().Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var personWriteOnlyRepository = new PersonWriteOnlyRepositoryBuilder().Build();
        var personReadOnlyRepository = new PersonReadOnlyRepositoryBuilder();
        var passwordEncrypter = new PasswordEncrypterBuild().Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();

        if (string.IsNullOrWhiteSpace(email) is false)
            userReadOnlyRepository.ExistActiveUserWithEmail(email);

        if (string.IsNullOrWhiteSpace(phoneNumber) is false)
            personReadOnlyRepository.ExistActiveUserWithPhoneNumber(phoneNumber);

        return new RegisterUserUseCase(
            unitOfWork,
            mapper,
            userWriteOnlyRepository,
            userReadOnlyRepository.Build(),
            personWriteOnlyRepository,
            personReadOnlyRepository.Build(),
            passwordEncrypter,
            tokenGenerator
        );
    }
}
