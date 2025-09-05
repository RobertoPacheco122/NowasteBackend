using System.Net;

namespace Nowaste.Exception.ExceptionBase;

public class NotFoundException(string message) : NowasteException(message) {
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors() {
        return [Message];
    }
}
