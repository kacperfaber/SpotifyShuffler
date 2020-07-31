using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Types
{
    public class PlaylistModelGenerator : IPlaylistModelGenerator
    {
        public PlaylistModel Generate(SimpleSpotifyPlaylist playlist)
        {
            return new PlaylistModel
            {
                SpotifyPlaylist = playlist
            };
        }
    }
}