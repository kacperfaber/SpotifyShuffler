using System.Diagnostics.CodeAnalysis;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistCollaborativeChecker : IPlaylistCollaborativeChecker
    {
        public bool Check(SpotifyAccount account, SpotifyPlaylist playlist)
        {
            bool isOwner = account.SpotifyId == playlist.Owner.Id;
            bool isCollab = playlist.Collaborative;

            return isCollab || isOwner;
        }
    }
}