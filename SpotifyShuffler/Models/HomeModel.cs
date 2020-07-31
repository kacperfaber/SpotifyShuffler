using System.Collections.Generic;

namespace SpotifyShuffler.Models
{
    public class HomeModel : LayoutModel
    {
        public IEnumerable<PlaylistModel> Playlists { get; set; }
        public int TotalPlaylists { get; set; }
    }
}