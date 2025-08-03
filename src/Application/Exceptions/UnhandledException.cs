namespace Application.Exceptions;

[Serializable]
public class UnhandledException : Exception
{
    public UnhandledException(string message)
        : base(message) { }

    public UnhandledException(string message, Exception innerException)
        : base(message, innerException) { }
}
