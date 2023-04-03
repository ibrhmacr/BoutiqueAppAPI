namespace Application.Exceptions;

public class UserRegisterFailedException : Exception
{
    public UserRegisterFailedException()
    {
    }

    public UserRegisterFailedException(string? message) : base(message)
    {
    }

    public UserRegisterFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}