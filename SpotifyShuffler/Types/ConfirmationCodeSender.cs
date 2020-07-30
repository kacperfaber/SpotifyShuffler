using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ConfirmationCodeSender : IConfirmationCodeSender
    {
        public IEmailSenderCredentialsGenerator CredentialsGenerator;
        public IEmailSenderSecretProvider SecretProvider;
        public IConfiguration Configuration;

        public ConfirmationCodeSender(IEmailSenderSecretProvider secretProvider, IEmailSenderCredentialsGenerator credentialsGenerator, IConfiguration configuration)
        {
            SecretProvider = secretProvider;
            CredentialsGenerator = credentialsGenerator;
            Configuration = configuration;
        }

        public Task SendAsync(ConfirmationCode confirmationCode)
        {
            if (!Configuration.GetValue<bool>("Emails:Sender:Settings:Send"))
            {
                return Task.Run(() => { });
            }
            
            return Task.Run(() =>
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = CredentialsGenerator.Generate(SecretProvider),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };
            
                MailAddress senderAddress = new MailAddress(SecretProvider.GetEmailAddress());
                MailAddress receiverAddress = new MailAddress(confirmationCode.Email);
            
                MailMessage message = new MailMessage(senderAddress, receiverAddress)
                {
                    IsBodyHtml = true,
                    Body = $"Hello, Your confirmation code is <strong>{confirmationCode.Code}</strong>",
                    Subject = "Confirm your email!"
                };
            
                client.Send(message);
            });
        }
    }
}