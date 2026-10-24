using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;

namespace PrimeShop.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpSettings _settings;

        public SmtpEmailService(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendContactNotificationAsync(
            ContactMessage contactMessage,
            CancellationToken cancellationToken = default)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_settings.FromEmail));
            email.To.Add(MailboxAddress.Parse(_settings.ToEmail));
            email.ReplyTo.Add(MailboxAddress.Parse(contactMessage.Email));
            email.Subject = $"New contact message: {contactMessage.Subject}";

            email.Body = new TextPart("plain")
            {
                Text =
                    $"Name: {contactMessage.Name}\n" +
                    $"Email: {contactMessage.Email}\n\n" +
                    $"Message:\n{contactMessage.Message}"
            };

            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync(
                _settings.Host,
                _settings.Port,
                SecureSocketOptions.Auto,
                cancellationToken);

            await smtpClient.AuthenticateAsync(
                _settings.UserName,
                _settings.Password,
                cancellationToken);

            await smtpClient.SendAsync(email, cancellationToken);
            await smtpClient.DisconnectAsync(true, cancellationToken);
        }
    }
}
