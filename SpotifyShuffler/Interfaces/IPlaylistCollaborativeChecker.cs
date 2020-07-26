using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface IPlaylistCollaborativeChecker
    {
        bool Check(User user, SpotifyPlaylist playlist);
    }
}