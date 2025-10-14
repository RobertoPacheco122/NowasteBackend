using System.Net;

namespace Nowaste.Exception.ExceptionBase;

public class ForbiddenException(string message) : NowasteException(message)
{
    public override int StatusCode => (int)HttpStatusCode.Forbidden;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
