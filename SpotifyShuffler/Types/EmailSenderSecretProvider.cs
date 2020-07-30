using Microsoft.Extensions.Configuration;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailSenderSecretProvider : IEmailSenderSecretProvider
    {
        public IConfiguration Configuration;

        public EmailSenderSecretProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GetUsername()
        {
            return Configuration["EmailSender:Credentials:Username"];
        }

        public string GetPassword()
        {
            return Configuration["EmailSender:Credentials:Password"];
        }

        public string GetEmailAddress()
        {
            return Configuration["EmailSender:Sender:EmailAddress"];
        }
    }
}