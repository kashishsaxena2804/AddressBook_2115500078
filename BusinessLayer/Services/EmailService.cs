using BusinessLayer.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class EmailServiceBL : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailServiceBL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendPasswordResetEmail(string email, string token)
        {
            string smtpServer = "smtp.sendgrid.net"; // SendGrid SMTP
            int smtpPort = 587;
            string smtpUser = "apikey"; // SendGrid recommends "apikey"
            string smtpPass = "YOUR_SENDGRID_API_KEY"; // Use actual API key

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Address Book System", "your-email@example.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Password Reset Request";

            message.Body = new TextPart("html")
            {
                Text = $"<p>Click the link to reset your password:</p> <a href='https://your-app.com/reset-password?token={token}'>Reset Password</a>"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(smtpUser, smtpPass);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
