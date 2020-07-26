using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface IPlaylistCollaborativeChecker
    {
        bool Check(SpotifyAccount account, SpotifyPlaylist playlist);
    }
}