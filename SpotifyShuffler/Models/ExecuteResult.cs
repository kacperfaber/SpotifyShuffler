using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Models
{
    public class ExecuteResult
    {
        public SpotifyPlaylist Playlist { get; set; }

        public Operation Operation { get; set; }
    }
}