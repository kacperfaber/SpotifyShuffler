using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Models
{
    public class PlaylistModel
    {
        public SimpleSpotifyPlaylist SpotifyPlaylist { get; set; }
        public string Description { get; set; }
    }
}