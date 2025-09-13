using CommonTestUtilities.Requests.Users;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.User.UpdateProfile;

public class UpdateProfileTest : NowasteClassFixture {
    private readonly string METHOD = "api/User";

    private readonly string _token;

    public UpdateProfileTest(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory) {
        _token = customWebApplicationFactory.CustomerUser.GetToken();
    }

    [Fact]
    public async Task Success() {
        var request = RequestUpdateUserProfileBuilder.Build();

        var response = await DoPut(requestUri: METHOD, request: request, token: _token);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_When_InstitutionId_And_EstablishmentId_Has_Value() {
        var request = RequestUpdateUserProfileBuilder.Build();
        request.InstitutionId = Guid.NewGuid();
        request.EstablishmentId = Guid.NewGuid();

        var response = await DoPut(requestUri: METHOD, request: request, token: _token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var parsedResponseBody = await JsonDocument.ParseAsync(responseBody);

        var errorMessages = parsedResponseBody.RootElement
            .GetProperty("errorMessages")
            .EnumerateArray();

        errorMessages.ShouldHaveSingleItem();
        errorMessages.ShouldContain(error => error.GetString()!.Equals("O usuário não pode estar associado a um estabelecimento e instituição ao mesmo tempo."));
    }

    [Fact]
    public async Task Error_When_Role_Is_Empty() {
        var request = RequestUpdateUserProfileBuilder.Build();
        request.Role = string.Empty;

        var response = await DoPut(requestUri: METHOD, request: request, token: _token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var parsedResponseBody = await JsonDocument.ParseAsync(responseBody);

        var errorMessages = parsedResponseBody.RootElement
            .GetProperty("errorMessages")
            .EnumerateArray();

        errorMessages.ShouldHaveSingleItem();
        errorMessages.ShouldContain(error => error.GetString()!.Equals("A role é obrigatória."));
    }


    [Fact]
    public async Task Error_When_Role_Is_Invalid() {
        var request = RequestUpdateUserProfileBuilder.Build();
        request.Role = "godMode";

        var response = await DoPut(requestUri: METHOD, request: request, token: _token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var parsedResponseBody = await JsonDocument.ParseAsync(responseBody);

        var errorMessages = parsedResponseBody.RootElement
            .GetProperty("errorMessages")
            .EnumerateArray();

        errorMessages.ShouldHaveSingleItem();
        errorMessages.ShouldContain(error => error.GetString()!.Equals("A role informada não é válida."));
    }
}
