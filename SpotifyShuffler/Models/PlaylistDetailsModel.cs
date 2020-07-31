using System.Collections.Generic;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Models
{
    public class PlaylistDetailsModel : LayoutModel
    {
        public SpotifyPlaylist Playlist { get; set; }
        public IEnumerable<Operation> Occurrences { get; set; }
        public IEnumerable<TrackModel> Tracks { get; set; }
    }
}