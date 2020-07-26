using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistCollaborativeChecker : IPlaylistCollaborativeChecker
    {
        public bool Check(User user, SpotifyPlaylist playlist)
        {
            bool isOwner = user.SpotifyAccount.SpotifyId == playlist.Owner.Id;
            bool isCollab = playlist.Collaborative;

            return isCollab || isOwner;
        }
    }
}