using Application.Abstractions;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Concretes;

public class MailService : IMailService
{
    public void SendMail(string to, string subject, string body, bool isBodyHtml = true)
    {
        //Etherreal.com
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("brielle.hartmann@ethereal.email"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls); 
        smtp.Authenticate("brielle.hartmann@ethereal.email","dagxfbuCryFskYpdVX");
        smtp.Send(email);
        smtp.Disconnect(true);
        
    }
}