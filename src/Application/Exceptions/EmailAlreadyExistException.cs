namespace Application.Exceptions;

[Serializable]
public class EmailAlreadyExistException : Exception
{
    public EmailAlreadyExistException(string message)
        : base(message) { }

    public EmailAlreadyExistException(string message, Exception innerException)
        : base(message, innerException) { }
}
