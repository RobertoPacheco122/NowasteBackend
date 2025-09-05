using CommonTestUtilities.Requests.Users;
using Nowaste.Application.UseCases.Users.Register;
using Nowaste.Domain.Enums;
using Shouldly;

namespace Validators.Test.Users.Register;

public class RegisterUserValidatorTests {
    [Fact]
    public void Success() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Error_When_Email_Is_Empty (string email) {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O email é obrigatório.");
    }

    [Fact]
    public void Error_When_Email_Is_Invalid() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "johndoe.com";

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O email não é válido.");
    }

    [Fact]
    public void Error_When_Password_Is_Empty() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "A senha deve ter no mínimo 8 " +
                "caracteres contendo pelo menos uma letra maiúscula, uma letra minúscula, um número " +
                "e um caractere especial (por exemplo, @, *, -, ?).");
    }

    [Fact]
    public void Error_When_Role_Is_Empty() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Role = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "A role é obrigatória.");
    }

    [Fact]
    public void Error_When_Role_Is_Invalid() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Role = "GodMode";

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "A role informada não é válida.");
    }

    [Fact]
    public void Error_When_InstitutionId_And_EstablishmentId_Have_Values() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.InstitutionId = Guid.NewGuid();
        request.EstablishmentId = Guid.NewGuid();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O usuário não pode estar associado a um estabelecimento e instituição ao mesmo tempo.");
    }

    [Fact]
    public void Error_When_Institution_User_Has_No_Institution_Role() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.InstitutionId = Guid.NewGuid();
        request.Role = Roles.ESTABLISHMENT_ADMIN;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "A role deve ser do tipo 'institution' quando InstitutionId estiver preenchido.");
    }

    [Fact]
    public void Error_When_FullName_Is_Empty() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.FullName = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O nome completo é obrigatório.");
    }

    [Fact]
    public void Error_When_PhoneNumber_Is_Empty() {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.PhoneNumber = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O celular é obrigatório");
    }

    [Theory]
    [InlineData("1")]
    [InlineData("11")]
    [InlineData("111")]
    [InlineData("1111")]
    [InlineData("11111")]
    [InlineData("111111")]
    [InlineData("1111111")]
    [InlineData("11111111")]
    [InlineData("111111111")]
    [InlineData("111111111111")]
    public void Error_When_PhoneNumber_Is_Invalid(string phoneNumber) {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.PhoneNumber = phoneNumber;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O celular deve ter no mínimo 10 digitos e no máximo 11 digitos.");
    }
}
