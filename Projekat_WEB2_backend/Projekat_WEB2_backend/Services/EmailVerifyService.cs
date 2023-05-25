using MailKit.Net.Smtp;
using MimeKit;
using Projekat_WEB2_backend.Enumerations;
using Projekat_WEB2_backend.Infrastructure.Configurations;
using Projekat_WEB2_backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Services
{
    public class EmailVerifyService : IEmailVerifyService
    {
        private readonly EmailVerifyConfiguration _emailConfig;

        public EmailVerifyService(EmailVerifyConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }


        public void SendVerificationMail(string prodavacMail, string statusVerifikacije)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_emailConfig.From));
            email.To.Add(MailboxAddress.Parse(prodavacMail));

            email.Subject = _emailConfig.Subject;

            string body = "";
            if (statusVerifikacije.Equals(StatusVerifikacije.Prihvacen.ToString()))
            {
                body = _emailConfig.MessageAccepted;
            }
            else if (statusVerifikacije.Equals(StatusVerifikacije.Odbijen.ToString()))
            {
                body = _emailConfig.MessageDenied;
            }


            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            using var smtp = new SmtpClient();
            smtp.Connect(_emailConfig.SmtpServer, _emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfig.UserName, _emailConfig.Password);
            smtp.Send(email);
            smtp.Disconnect(true);


        }

    }
}
