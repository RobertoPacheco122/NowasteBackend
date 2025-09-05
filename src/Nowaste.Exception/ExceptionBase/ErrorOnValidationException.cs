using System.Net;

namespace Nowaste.Exception.ExceptionBase;

public class ErrorOnValidationException(List<string> errorMessages) : NowasteException(string.Empty)
{
    private readonly List<string> _errors = errorMessages;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors() {
        return _errors;
    }
}
