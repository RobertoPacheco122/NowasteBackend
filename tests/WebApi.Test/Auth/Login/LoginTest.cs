using Nowaste.Communication.Requests.Auth;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Auth.Login;

public class LoginTest : NowasteClassFixture {
    private readonly string METHOD = "api/Auth/Login";

    private readonly string _email;
    private readonly string _fullName;
    private readonly string _password;

    public LoginTest(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory) {
        _email = customWebApplicationFactory.CustomerUser.GetEmail();
        _fullName = customWebApplicationFactory.CustomerUser.GetFullName();
        _password = customWebApplicationFactory.CustomerUser.GetPassword();
    }

    [Fact]
    public async Task Success() {
        var request = new RequestLoginJson {
            Email = _email,
            Password = _password,
        };

        var response = await DoPost(requestUri: METHOD, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var parsedResponseBody = await JsonDocument.ParseAsync(responseBody);

        parsedResponseBody.RootElement
            .GetProperty("name")
            .GetString()
            .ShouldBe(_fullName.Split(" ").First());
        parsedResponseBody.RootElement
            .GetProperty("token")
            .GetString()
            .ShouldNotBeNullOrEmpty();
    }
}
