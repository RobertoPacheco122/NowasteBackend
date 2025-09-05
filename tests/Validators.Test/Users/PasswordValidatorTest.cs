using CommonTestUtilities.Requests.Users;
using FluentValidation;
using Nowaste.Application.UseCases.Users;
using Nowaste.Application.UseCases.Users.Register;
using Nowaste.Communication.Requests.Users;
using Shouldly;

namespace Validators.Test.Users;

public class PasswordValidatorTest {
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Error_When_Password_Is_Invalid(string password) {
        var validator = new PasswordValidator<RequestRegisterUserJson>();

        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);

        result.ShouldBeFalse();
    }
}
