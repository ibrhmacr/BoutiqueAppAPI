namespace Application.Exceptions;

public class PasswordChangeFailedException : Exception
{
    public PasswordChangeFailedException()
    {
    }

    public PasswordChangeFailedException(string? message) : base(message)
    {
    }

    public PasswordChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}