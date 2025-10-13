using CommonTestUtilities.Requests.Address;
using Nowaste.Application.UseCases.Address.Register;
using Shouldly;

namespace Validators.Test.Address.Register;

public class RegisterAddressValidatorTest {
    [Fact]
    public void Success() {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Error_When_Street_Name_Is_Empty(string streetName) {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.StreetName = streetName;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O endereço é obrigatório.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Error_When_Number_Is_Empty(string number) {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.Number = number;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O número do endereço é obrigatório.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Error_When_City_Is_Empty(string city) {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.City = city;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "A cidade é obrigatória.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Error_When_State_Is_Empty(string state) {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.State = state;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O estado é obrigatória.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Error_When_Zip_Code_Is_Empty(string zipCode) {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.ZipCode = zipCode;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O CEP é obrigatório.");
    }

    [Theory]
    [InlineData("ABCDEFGH")]
    [InlineData("123a6789")]
    [InlineData("123A6789")]
    public void Error_When_Zip_Code_Has_Letters(string zipCode) {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.ZipCode = zipCode;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O CEP não pode conter letras.");
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("1234567")]
    [InlineData("123456789")]
    public void Error_When_Zip_Code_Not_Have_Eight_Characters(string zipCode) {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.ZipCode = zipCode;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "O CEP deve ter 8 caracteres.");
    }

    [Fact]
    public void Error_When_No_Relation_Id_Is_Gives() {
        var validator = new RegisterAddressValidator();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.PersonId = null;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage == "Pelo menos um dos seguintes campos deve ser preenchido: PersonId, EstablishmentId, ou InstitutionId.");
    }
}
