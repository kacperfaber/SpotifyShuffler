using System.Net;

namespace SpotifyShuffler.Interfaces
{
    public interface IEmailSenderCredentialsGenerator
    {
        NetworkCredential Generate(IEmailSenderSecretProvider secretProvider);
    }
}