using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test;

public class NowasteClassFixture(CustomWebApplicationFactory customWebApplicationFactory) : IClassFixture<CustomWebApplicationFactory> {
    private readonly HttpClient _httpClient = customWebApplicationFactory.CreateClient();
    private readonly string _token = customWebApplicationFactory.CustomerUser.GetToken();
    
    protected async Task<HttpResponseMessage> DoPost(
        string requestUri,
        object request,
        string token = ""
    ) {
        AuthorizeToken(token);

        return await _httpClient.PostAsJsonAsync(requestUri, request);
    }

    protected async Task<HttpResponseMessage> DoGet(
        string requestUri,
        string token
    ) {
        AuthorizeToken(token);

        return await _httpClient.GetAsync(requestUri);
    }

    private void AuthorizeToken(string token) {
        if (string.IsNullOrWhiteSpace(token))
            return;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
    }
}
