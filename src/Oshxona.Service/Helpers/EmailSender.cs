using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Oshxona.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshxona.Service.Helpers
{
    public class EmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration.GetSection("Email");
        }
        public async Task SendAsync(Message message)
        {
            var email = new MimeMessage();
            email.To.Add(MailboxAddress.Parse(message.To));
            email.From.Add(MailboxAddress.Parse(configuration["Address"]));
            email.Subject = message.Subject;
            email.Body = new TextPart("html") { Text = message.Body };

            var sender = new SmtpClient();
            await sender.ConnectAsync(this.configuration["Host"], 587, SecureSocketOptions.StartTls);
            await sender.AuthenticateAsync(this.configuration["Address"], this.configuration["Password"]);
            await sender.SendAsync(email);
            await sender.DisconnectAsync(true);
        }
    }
}
