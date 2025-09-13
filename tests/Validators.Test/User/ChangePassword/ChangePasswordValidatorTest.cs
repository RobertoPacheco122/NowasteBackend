using CommonTestUtilities.Requests.Users;
using Nowaste.Application.UseCases.Users.ChangePassword;
using Shouldly;

namespace Validators.Test.User.ChangePassword;

public class ChangePasswordValidatorTest {
    [Fact]
    public void Success() {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Error_New_Password_Empty(string newPassword) {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordBuilder.Build();
        request.NewPassword = newPassword;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "A senha deve ter no mínimo 8 " +
            "caracteres contendo pelo menos uma letra maiúscula, uma letra minúscula, um número " +
            "e um caractere especial (por exemplo, @, *, -, ?).");
    }
}
