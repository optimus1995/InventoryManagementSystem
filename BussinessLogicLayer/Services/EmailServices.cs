using System.Runtime.CompilerServices;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Infrastructure.Services
{
    public class EmailServices : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emaillog = new MimeMessage();
            emaillog.From.Add(MailboxAddress.Parse("usamaishtiaq85@gmail.com"));
            emaillog.To.Add(MailboxAddress.Parse(email));
            emaillog.Subject = subject;
            emaillog.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            using var client = new SmtpClient();
            // client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("usamaishtiaq85@gmail.com", "wmof jmhy rdoh qybg");
            client.Send(emaillog);
            client.Disconnect(true);
        }
    }
}
