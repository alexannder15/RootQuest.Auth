namespace Application.Exceptions;

public class IocNotFoundException : Exception
{
    public IocNotFoundException(string message)
        : base(message) { }

    public IocNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }
}
