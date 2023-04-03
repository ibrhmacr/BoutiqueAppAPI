namespace Application.Abstractions;

public interface IMailService
{
    void SendMail(string to, string subject, string body, bool isBodyHtml = true);
    
}