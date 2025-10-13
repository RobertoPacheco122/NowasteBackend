using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Address;
using CommonTestUtilities.Requests.Address;
using Nowaste.Application.UseCases.Address.Register;
using Nowaste.Exception.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Address.Register;

public class RegisterAddressUseCaseTest {
    [Fact]
    public async Task Success() {
        var useCase = CreateUseCase();

        var request = RequestRegisterAddressJsonBuilder.Build();

        var response = await useCase.Execute(request);

        response.ShouldNotBeNull();
        response.StreetName.ShouldBe(request.StreetName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Error_When_Street_Name_Is_Empty(string streetName) {
        var useCase = CreateUseCase();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.StreetName = streetName;

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O endereço é obrigatório."));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Error_When_Number_Is_Empty(string number) {
        var useCase = CreateUseCase();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.Number = number;

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O número do endereço é obrigatório."));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Error_When_City_Is_Empty(string city) {
        var useCase = CreateUseCase();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.City = city;

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("A cidade é obrigatória."));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Error_When_State_Is_Empty(string state) {
        var useCase = CreateUseCase();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.State = state;

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O estado é obrigatória."));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Error_When_Zip_Code_Is_Empty(string zipCode) {
        var useCase = CreateUseCase();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.ZipCode = zipCode;

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O CEP é obrigatório."));
    }

    [Theory]
    [InlineData("ABCDEFGH")]
    [InlineData("123a6789")]
    [InlineData("123A6789")]
    public async Task Error_When_Zip_Code_Has_Letters(string zipCode) {
        var useCase = CreateUseCase();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.ZipCode = zipCode;

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O CEP não pode conter letras."));
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
    public async Task Error_When_Zip_Code_Not_Have_Eight_Characters(string zipCode) {
        var useCase = CreateUseCase();

        var request = RequestRegisterAddressJsonBuilder.Build();
        request.ZipCode = zipCode;

        var act = async () => await useCase.Execute(request);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        var errorsMessages = result.GetErrors();

        errorsMessages.ShouldHaveSingleItem();
        errorsMessages.ShouldContain(e => e.Equals("O CEP deve ter 8 caracteres."));
    }

    private static RegisterAddressUseCase CreateUseCase() {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        var addressWriteOnlyRepository = new AddressWriteOnlyRepositoryBuilder().Build();

        return new RegisterAddressUseCase(
            unitOfWork,
            mapper,
            addressWriteOnlyRepository
        );
    }
}
