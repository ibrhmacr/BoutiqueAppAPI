namespace Application.Abstractions;

public interface IMailService
{
    void SendMail(string to, string subject, string body);

    void SendEmailConfirmationMail(string to, int userId, string verificationToken);

    void SendPasswordResetMailAsync(string to, int userId, string passwordResetToken);

    void SendCreateOrderMailAsync(string to, string orderCode, string username, string addressName);

}