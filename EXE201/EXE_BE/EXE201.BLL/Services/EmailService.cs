using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.EmailDTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Net;
using EXE201.DAL.Models;

namespace EXE201.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;

        public EmailService(IOptions<EmailSetting> emailSettings)
        {
            _emailSetting = emailSettings.Value;
        }

        public async Task SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSetting.Email);
            email.From.Add(MailboxAddress.Parse(_emailSetting.Email));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = request.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            smtp.Connect(_emailSetting.Host, _emailSetting.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSetting.Email, _emailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task<bool> SendEmailAsync(EmailDTO emailDto)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSetting.DisplayName, _emailSetting.Email));
            emailMessage.To.Add(new MailboxAddress("", emailDto.ToEmail));
            emailMessage.Subject = emailDto.Subject;
            emailMessage.Body = new TextPart("html") { Text = emailDto.Body };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailSetting.Host, _emailSetting.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(new NetworkCredential(_emailSetting.Email, _emailSetting.Password));
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
