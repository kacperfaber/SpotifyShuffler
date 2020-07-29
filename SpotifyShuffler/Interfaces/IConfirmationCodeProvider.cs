using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IConfirmationCodeProvider
    {
        ConfirmationCode Provide(string email, string code);
    }
}