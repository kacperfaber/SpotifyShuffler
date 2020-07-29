using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IEmailAddressProvider
    {
        EmailAddress Provide(User user);
    }
}