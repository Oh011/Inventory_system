using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Shared.Dtos;
using Shared.Options;


namespace Infrastructure.Services
{
    public class MailKitEmailService : IEmailService
    {


        private readonly IOptions<SmtpOptions> _options;



        public MailKitEmailService(IOptions<SmtpOptions> options)
        {
            _options = options;
        }




        public async Task SendEmailAsync(EmailMessage message)
        {

            //1- Creates the email message object — from the MimeKit library
            var email = new MimeMessage();

            var options = _options.Value;

            email.From.Add(new MailboxAddress(options.DisplayName, options.From));
            email.To.Add(MailboxAddress.Parse(message.To));
            email.Subject = message.Subject;


            //2-  Builds the HTML body of the email.
            var builder = new BodyBuilder { HtmlBody = message.Body };



            foreach (var attachment in message.Attachments)
            {
                builder.Attachments.Add(attachment.FileName, attachment.Content, ContentType.Parse(attachment.ContentType));
            }

            email.Body = builder.ToMessageBody();

            //3-  Creates the SMTP client from MailKit.Net.Smtp, not the old System.Net.Mail.
            using var smtp = new SmtpClient();


            //4- Connects to the SMTP server using the given:

            await smtp.ConnectAsync(options.Host, options.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(options.UserName, options.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
