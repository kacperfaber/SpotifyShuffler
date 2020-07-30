using System.Net;
using Microsoft.Extensions.Configuration;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailSenderCredentialsGenerator : IEmailSenderCredentialsGenerator
    {
        public NetworkCredential Generate(IEmailSenderSecretProvider secretProvider)
        {
            return new NetworkCredential
            {
                UserName = secretProvider.GetUsername(),
                Password = secretProvider.GetPassword()
            };
        }
    }
}