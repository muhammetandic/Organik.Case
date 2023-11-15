

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Organik.Case.Application.Helpers;
using Organik.Case.Application.Interfaces.ExternalServices;

namespace Organik.Case.Infrastructure.ExternalServices
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }

        public void SendMail(string to, string name, string code)
        {
            var from = _mailSettings.From;
            var subject = "Erişim Kodu";
            var body = $"Sisteme erişim kodunuz: {code}";
            var message = new MailMessage(from, to, subject, body);

            using var client = new SmtpClient(_mailSettings.SmtpServer)
            {
                Port = _mailSettings.Port,
                Credentials = new NetworkCredential(_mailSettings.Username, _mailSettings.Password),
                EnableSsl = true,
            };
            client.Send(from, to, subject, body);
        }

    }
}