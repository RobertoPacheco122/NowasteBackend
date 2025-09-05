using Nowaste.Domain.Security.Tokens;

namespace Nowaste.Api.Token;

public class HttpContextTokenValue(IHttpContextAccessor httpContextAccessor) : ITokenProvider {
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string TokenOnRequest() {
        var authorization = _httpContextAccessor
            .HttpContext!
            .Request
            .Headers
            .Authorization
            .ToString();

        return authorization.Replace("Bearer ", "").Trim();
    }
}
