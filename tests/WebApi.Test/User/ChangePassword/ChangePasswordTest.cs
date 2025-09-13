using CommonTestUtilities.Requests.Users;
using Nowaste.Communication.Requests.Auth;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.User.ChangePassword;
public class ChangePasswordTest : NowasteClassFixture {
    private readonly string METHOD = "api/User/change-password";
    private readonly string LOGIN_METHOD = "api/Auth/Login";

    private readonly string _token;
    private readonly string _email;
    private readonly string _password;

    public ChangePasswordTest(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory) {
        _token = customWebApplicationFactory.CustomerUser.GetToken();
        _email = customWebApplicationFactory.CustomerUser.GetEmail();
        _password = customWebApplicationFactory.CustomerUser.GetPassword();
    }

    [Fact]
    public async Task Success() {
        var request = RequestChangePasswordBuilder.Build();
        request.OldPassword = _password;

        var response = await DoPut(requestUri: METHOD, request: request, token: _token);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var loginRequest = new RequestLoginJson {
            Email = _email,
            Password = _password,
        };

        var loginResponse = await DoPost(requestUri: LOGIN_METHOD, request: loginRequest);
        loginResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        loginRequest.Password = request.NewPassword;

        loginResponse = await DoPost(requestUri: LOGIN_METHOD, request: loginRequest);
        loginResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Error_When_Old_Password_Is_Different_From_Current_Password() {
        var request = RequestChangePasswordBuilder.Build();

        var response = await DoPut(requestUri: METHOD, request: request, token: _token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var parsedResponseBody = await JsonDocument.ParseAsync(responseBody);

        var errorMessages = parsedResponseBody.RootElement
            .GetProperty("errorMessages")
            .EnumerateArray();

        errorMessages.ShouldHaveSingleItem();
        errorMessages.ShouldContain(error => error.GetString()!.Equals("A senha informada é diferente da senha atual."));
    }
}
