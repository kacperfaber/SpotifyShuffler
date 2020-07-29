using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface ISpotifyEmailIsSameChecker
    {
        bool Check(EmailAddress emailAddress, SpotifyAccount spotifyAccount);
    }
}