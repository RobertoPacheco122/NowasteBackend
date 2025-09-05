namespace Nowaste.Domain.Security.Tokens;

public interface ITokenProvider {
    string TokenOnRequest();
}
