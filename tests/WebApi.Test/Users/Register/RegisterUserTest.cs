using CommonTestUtilities.Requests.Users;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Users.Register;

public class RegisterUserTest(CustomWebApplicationFactory customWebApplicationFactory) : NowasteClassFixture(customWebApplicationFactory) {
    private readonly string METHOD = "api/Users";

    [Fact]
    public async Task Success() {
        var request = RequestRegisterUserJsonBuilder.Build();

        var response = await DoPost(requestUri: METHOD, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var parsedResponseBody = await JsonDocument.ParseAsync(responseBody);

        parsedResponseBody.RootElement
            .GetProperty("name")
            .GetString()
            .ShouldBe(request.FullName.Split(" ").First());
        parsedResponseBody.RootElement
            .GetProperty("token")
            .GetString()
            .ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_When_Email_Is_Empty() {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var response = await DoPost(METHOD, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var parsedResponseBody = await JsonDocument.ParseAsync(responseBody);

        var errorMessages = parsedResponseBody.RootElement
            .GetProperty("errorMessages")
            .EnumerateArray();

        errorMessages.ShouldHaveSingleItem();
        errorMessages.ShouldContain(error => error.GetString()!.Equals("O email é obrigatório."));
    }
}
