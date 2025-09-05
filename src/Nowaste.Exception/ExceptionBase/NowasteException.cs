namespace Nowaste.Exception.ExceptionBase;

public abstract class NowasteException(string message) : SystemException(message) {
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
