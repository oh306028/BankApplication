using System.Net.Mail;
using System.Net;

namespace BankApplication.App.Services.Client
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string toEmail, string code);
        Task SendClientCodeMail(string toEmail, string clientCode);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendVerificationEmailAsync(string toEmail, string code)
        {
            var smtpClient = new SmtpClient(_config["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_config["EmailSettings:Port"]),
                Credentials = new NetworkCredential(
                    _config["EmailSettings:Username"],
                    _config["EmailSettings:Password"]
                ),
                EnableSsl = bool.Parse(_config["EmailSettings:EnableSsl"])
            };

            var message = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:FromEmail"], _config["EmailSettings:FromName"]),
                Subject = "Kod weryfikacyjny",
                Body = $"Twój kod weryfikacyjny: {code}",
                IsBodyHtml = false
            };

            message.To.Add(toEmail);

            await smtpClient.SendMailAsync(message);
        }

        public async Task SendClientCodeMail(string toEmail, string clientCode)     
        {
            var smtpClient = new SmtpClient(_config["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_config["EmailSettings:Port"]),
                Credentials = new NetworkCredential(
                    _config["EmailSettings:Username"],
                    _config["EmailSettings:Password"]
                ),
                EnableSsl = bool.Parse(_config["EmailSettings:EnableSsl"])
            };

            var message = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:FromEmail"], _config["EmailSettings:FromName"]),
                Subject = "Kod klienta PocketBank",
                Body = $"Dziękujemy za dołączenie do PocketBank! Zaakceptowaliśmy Twój wniosek. Utwórz teraz konto bankowe! Twój unikalny kod Klienta: {clientCode}",  
                IsBodyHtml = false
            };

            message.To.Add(toEmail);

            await smtpClient.SendMailAsync(message);
        }
    }
}
