using System.Net;

namespace Nowaste.Exception.ExceptionBase;

public class InvalidLoginException(string message) : NowasteException(message) {
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors() {
        return [Message];
    }
}
