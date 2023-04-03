namespace Application.Exceptions;

public class InvalidExternalAuthenticationException : Exception
{
    public InvalidExternalAuthenticationException()
    {
    }

    public InvalidExternalAuthenticationException(string? message) : base(message)
    {
    }

    public InvalidExternalAuthenticationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}