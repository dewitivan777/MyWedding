using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;

namespace Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(List<string> email, string subject, string message, string attachment = null)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    foreach (var address in email)
                    {
                        emailMessage.To.Add(new MailAddress(address));
                    }
                    emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    if (attachment != null)
                    {
                        emailMessage.Attachments.Add( new Attachment(attachment));
                    }
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }


        public async Task SendLogin(string email, string subject, string message)
        {
            var emailMessage = $"To: {email}\nSubject: {subject}\nMessage: {message}\n\n";

            File.AppendAllText("emails.txt", emailMessage);

            await Task.CompletedTask;
        }


    }
}