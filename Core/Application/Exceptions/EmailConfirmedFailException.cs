namespace Application.Exceptions;

public class EmailConfirmedFailException : Exception
{
    public EmailConfirmedFailException()
    {
    }

    public EmailConfirmedFailException(string? message) : base(message)
    {
    }

    public EmailConfirmedFailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}