using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IUserGenerator
    {
        User GenerateUser(string username, string emailAddress);
    }
}