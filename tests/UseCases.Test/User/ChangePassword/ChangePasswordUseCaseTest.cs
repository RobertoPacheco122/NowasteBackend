using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.User;
using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Services.LoggedUser;
using Nowaste.Application.UseCases.Users.ChangePassword;
using Nowaste.Domain.Entities;
using Nowaste.Exception.ExceptionBase;
using Shouldly;

namespace UseCases.Test.User.ChangePassword;

public class ChangePasswordUseCaseTest {
    [Fact]
    public async Task Success() {
        var user = UserBuilder.Build();

        var request = RequestChangePasswordBuilder.Build();

        var useCase = CreateUseCase(user, request.OldPassword);

        var act = async () => await useCase.Execute(request);

        await act.ShouldNotThrowAsync();
    }

    [Fact]
    public async Task Error_When_New_Password_Is_Empty() {
        var user = UserBuilder.Build();

        var request = RequestChangePasswordBuilder.Build();
        request.NewPassword = string.Empty;

        var useCase = CreateUseCase(user, request.OldPassword);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errors = result.GetErrors();

        errors.ShouldHaveSingleItem();
        errors.ShouldContain(e => e.Equals("A senha deve ter no mínimo 8 " +
            "caracteres contendo pelo menos uma letra maiúscula, uma letra minúscula, um número " +
            "e um caractere especial (por exemplo, @, *, -, ?)."));
    }

    [Fact]
    public async Task Error_When_Old_Password_Is_Different() {
        var user = UserBuilder.Build();

        var request = RequestChangePasswordBuilder.Build();

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errors = result.GetErrors();

        errors.ShouldHaveSingleItem();
        errors.ShouldContain(e => e.Equals("A senha informada é diferente da senha atual."));
    }

    private static ChangePasswordUseCase CreateUseCase(UserEntity user, string? password = null) {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var userUpdateOnlyRepository = UserUpdateOnlyRepository.Build(user);
        var passwordEncrypter = new PasswordEncrypterBuild().Verify(password).Build();

        return new ChangePasswordUseCase(
           unitOfWork,
           loggedUser,
           userUpdateOnlyRepository,
           passwordEncrypter
        );
    }
}
