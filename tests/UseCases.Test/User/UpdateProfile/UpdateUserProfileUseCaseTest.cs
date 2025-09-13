using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Person;
using CommonTestUtilities.Repositories.User;
using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Services.LoggedUser;
using Nowaste.Application.UseCases.Users.UpdateProfile;
using Nowaste.Communication.Enums;
using Nowaste.Domain.Entities;
using Nowaste.Exception.ExceptionBase;
using Shouldly;

namespace UseCases.Test.User.UpdateProfile;

public class UpdateUserProfileUseCaseTest {
    [Fact]
    public async Task Success() {
        var user = UserBuilder.Build();

        var request = RequestUpdateUserProfileBuilder.Build();

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.ShouldNotThrowAsync();
    }

    [Fact]
    public async Task Error_When_InstitutionId_And_EstablishmentId_Has_Value() {
        var user = UserBuilder.Build();

        var request = RequestUpdateUserProfileBuilder.Build();
        request.EstablishmentId = Guid.NewGuid();
        request.InstitutionId = Guid.NewGuid();

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O usuário não pode estar associado a um estabelecimento e instituição ao mesmo tempo."));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Error_When_Role_Empty(string role) {
        var user = UserBuilder.Build();

        var request = RequestUpdateUserProfileBuilder.Build();
        request.Role = role;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("A role é obrigatória."));
    }

    [Fact]
    public async Task Error_When_Role_Is_Invalid() {
        var user = UserBuilder.Build();

        var request = RequestUpdateUserProfileBuilder.Build();
        request.Role = "godMode";

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("A role informada não é válida."));
    }

    [Fact]
    public async Task Error_When_User_Status_Is_Invalid() {
        var user = UserBuilder.Build();

        var request = RequestUpdateUserProfileBuilder.Build();
        request.UserStatus = (EUserStatus)99;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O status do usuário é inválido."));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Error_When_Phone_Number_Is_Empty(string phoneNumber) {
        var user = UserBuilder.Build();

        var request = RequestUpdateUserProfileBuilder.Build();
        request.PhoneNumber = phoneNumber;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O celular é obrigatório."));
    }

    private static UpdateUserProfileUseCase CreateUseCase(UserEntity user) {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var userUpdateOnlyRepository = UserUpdateOnlyRepository.Build(user);
        var personReadOnlyRepository = new PersonReadOnlyRepositoryBuilder().Build();

        return new UpdateUserProfileUseCase(
            unitOfWork,
            loggedUser,
            userUpdateOnlyRepository,
            personReadOnlyRepository
        );
    }
}
