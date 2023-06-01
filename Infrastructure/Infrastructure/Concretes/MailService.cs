using System.Text;
using Application.Abstractions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Concretes;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMail(string to, string subject, string body)
    {
        //Etherreal.com
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("cecilia.toy65@ethereal.email"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls); 
        smtp.Authenticate("cecilia.toy65@ethereal.email","Y5rRa5qTWghHtKqTu2");
        smtp.Send(email);
        smtp.Disconnect(true);
        
    }

    public void SendEmailConfirmationMail(string to, int userId, string verificationToken)
    {
        StringBuilder mail = new();
        mail.AppendLine("Merhaba<br>Thank you for your register for Boutiqe<br><strong><a target=\"_blank\" href=\""); //
        mail.AppendLine(_configuration["URLs:ClientURL"]);
        mail.AppendLine("api/Users/verify-email?userId=");
        mail.AppendLine($"{userId}");
        mail.AppendLine("&verificationCode=");
        mail.AppendLine(verificationToken);
        mail.AppendLine("\">Verify your email</a></strong><br><br<br><br>BoutiqueApp");

        SendMail(to, "Confirm Email Request", mail.ToString());
    }

    public void SendPasswordResetMailAsync(string to, int userId, string passwordResetToken)
    {
        StringBuilder mail = new();
        mail.AppendLine("Hi We are reaching to you from BoutiqueApp for your Password Reset request." +
                        " You can easily reset your password from below Link and " +
                        "you can sure We handle every security steps for your safety :)<br>");
        mail.AppendLine("<strong><a target=\"_blank\" href=\"");
        mail.AppendLine(_configuration["URLs:ClientURL"]);
        mail.AppendLine("api/Users/password-reset?userId=");
        mail.AppendLine($"{userId}");
        mail.AppendLine("&passwordResetToken=");
        mail.AppendLine(passwordResetToken);
        mail.AppendLine("\">Reset Password</a></strong><br><br<br><br>BoutiqueApp");
        
        SendMail(to,"Reset Password BoutiqueApp", mail.ToString());
    }

    public void SendCreateOrderMailAsync(string to, string orderCode, string username, string addressName)
    {
        StringBuilder mail = new();
        mail.AppendLine("Hi ");
        mail.AppendLine($"{username},<br>");
        mail.AppendLine($"{orderCode}");
        mail.AppendLine("teslimat numarali siparisiniz Boutique Kargo tarafindan ");
        mail.AppendLine($"{addressName}");
        mail.AppendLine(" e dogru yola cikmistir");
        mail.AppendLine("<br>Siparisinizin kargo takibini teslimat numarasiyla yapabilirsiniz");
        mail.AppendLine(("Bizi tercih ettiginiz icin tesekkur ederiz.<br> <strong>BoutiqueApp</strong>"));
        
        SendMail(to,"BoutiqueApp Siparis Onayi", mail.ToString());
    }
}