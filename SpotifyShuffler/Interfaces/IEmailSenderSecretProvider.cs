namespace SpotifyShuffler.Interfaces
{
    public interface IEmailSenderSecretProvider
    {
        string GetUsername();

        string GetPassword();

        string GetEmailAddress();
    }
}