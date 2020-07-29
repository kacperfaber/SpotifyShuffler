using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IConfirmationCodeGenerator
    {
        ConfirmationCode Generate(string email);
    }
}