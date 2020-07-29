using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IEmailAddressGenerator
    {
        EmailAddress Generate(User user, string email);
    }
}